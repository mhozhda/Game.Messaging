using Game.Messaging.Server;
using Game.Messaging.Server.Application.Common.Mapping;
using Game.Messaging.Server.Application.GameOffers.Commands;
using Game.Messaging.Server.Infrastructure.Persistance;
using Game.Messaging.Server.Infrastructure.UserNotifications;
using Game.Messaging.Server.Infrastructure.UserNotifications.SignalR.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s => 
{ 
	s.CustomSchemaIds(x => x.FullName);
	s.SwaggerDoc(
		"v1", 
		new OpenApiInfo { 
			Title = "Game Messaging",
			Description = $"Available teams: {Constants.Users.Teams.Lions} | {Constants.Users.Teams.Bears} | {Constants.Users.Teams.Crocodiles}"
		}
	); 
});

builder.Services.AddSignalR();
builder.Services.AddDbContext<GameEventsDbContext>(options => options.UseInMemoryDatabase("GameEvents"));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(AddGameOffer)));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Logging.AddConsole();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserNotificationService, UserNotificationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseRouting();
app.UseExceptionHandler("/error");

// Separate connecting signalR users into different teams
app.Use((context, next) =>
{
	if (context.Request.Path == "/gamemessagehub")
	{
		var claims = new List<Claim>();

		var teams = new List<string> { Constants.Users.Teams.Bears, Constants.Users.Teams.Lions, Constants.Users.Teams.Crocodiles };
		var index = new Random().Next(0, teams.Count);
		claims.Add(new Claim(Constants.Claims.TeamClaim, teams[index]));
		claims.Add(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));

		var claimIdentity = new ClaimsIdentity(claims);

		context.User = new ClaimsPrincipal(claimIdentity);
	}

	return next();
});

app.UseEndpoints(enpoints =>
{
	enpoints.MapControllers();
	enpoints.MapHub<GameMessageHub>("/gamemessagehub");
});

app.Run();
