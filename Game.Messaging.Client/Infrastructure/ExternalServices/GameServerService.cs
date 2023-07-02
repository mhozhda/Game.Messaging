using Game.Messaging.Client.Dtos;
using Game.Messaging.Client.Entities;
using Game.Messaging.Client.Exceptions;
using Game.Messaging.Client.Options;
using System.Net.Http.Json;

namespace Game.Messaging.Client.Infrastructure.ExternalServices
{
	public class GameServerService
	{
		private readonly HttpClient _client;
		private readonly GameServerApiOptions _gameServerApiOptions;

		public GameServerService(GameServerApiOptions options)
		{
			_gameServerApiOptions = options;
			_client = new();
		}

		public async Task<List<GameOffer>> GetGameOffersAsync()
		{
			try
			{
				var response = await _client.GetFromJsonAsync<PagingResponse<GameOffer>>(_gameServerApiOptions.GameOffersApiPath);

				return response.Results;
			}
			catch (Exception ex)
			{
				throw new RemoteServiceRequestException($"Getting game offers failed. Error: {ex}");
			}
		}

		public async Task<List<GameEvent>> GetGameEventsAsync()
		{
			try
			{
				var response = await _client.GetFromJsonAsync<PagingResponse<GameEvent>>(_gameServerApiOptions.GameEventsApiPath);

				return response.Results;
			}
			catch (Exception ex)
			{
				throw new RemoteServiceRequestException($"Getting game events failed. Error: {ex}");
			}
		}
	}
}
