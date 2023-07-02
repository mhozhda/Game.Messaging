using Game.Messaging.Server.Application.Common.Mapping;
using Game.Messaging.Server.Application.GameOffers.Commands;
using Game.Messaging.Server.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GameEventsDbContext>(options => options.UseInMemoryDatabase("GameEvents"));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(AddGameOffer)));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

builder.Services.AddSwaggerGen(s => s.CustomSchemaIds(x => x.FullName));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
