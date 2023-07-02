using Game.Messaging.Client.Entities;
using Game.Messaging.Client.Infrastructure.ExternalServices;
using Game.Messaging.Client.Options;
using Microsoft.AspNetCore.SignalR.Client;


var apiUrl = "http://localhost:5201/gamemessagehub";
var gameServerApiOptions = new GameServerApiOptions
{
	GameEventsApiPath = "http://localhost:5201/gameevents",
	GameOffersApiPath = "http://localhost:5201/gameoffers"
};

try
{
	var _gameServerService = new GameServerService(gameServerApiOptions);

	Console.WriteLine("Current Offers:");
	var currentOffers = await _gameServerService.GetGameOffersAsync();
	foreach (var offer in currentOffers)
	{
		Console.WriteLine(offer);
	}

	Console.WriteLine("Current Events:");
	var currentEvents = await _gameServerService.GetGameEventsAsync();
	foreach (var @event in currentEvents)
	{
		Console.WriteLine(@event);
	}

	var connection = new HubConnectionBuilder()
		.WithUrl(apiUrl)
		.Build();

	connection.On<GameOffer>("New Offer Added", offer =>
	{
		Console.WriteLine($"New Offer: {offer}");
	});

	connection.On<GameEvent>("New Event Added", @event =>
	{
		Console.WriteLine($"New Event:{@event}");
	});

	connection.StartAsync().Wait();

	Console.WriteLine("Listening for new Offers and Events...");
	Console.WriteLine("Press any key to stop listening.");

	Console.ReadKey();

	await connection.StopAsync();
}
catch (Exception ex)
{
	Console.WriteLine($"Error occured: {ex}");
}
