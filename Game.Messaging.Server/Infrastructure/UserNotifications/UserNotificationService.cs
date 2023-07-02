using Game.Messaging.Server.Entities;
using Game.Messaging.Server.Infrastructure.UserNotifications.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Game.Messaging.Server.Infrastructure.UserNotifications
{
	public class UserNotificationService : IUserNotificationService
	{
		private readonly IHubContext<GameMessageHub> _hubContext;

		public UserNotificationService(IHubContext<GameMessageHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async Task SendGameOfferToAllAsync(GameOffer gameOffer)
		{
			await _hubContext.Clients.All.SendAsync("NewOfferAdded", gameOffer);
		}

		public async Task SendGameOfferToTeamAsync(GameOffer gameOffer, string teamName)
		{
			await _hubContext.Clients.Group(teamName).SendAsync("NewOfferAdded", gameOffer);
		}

		public async Task SendGameEventToAllAsync(GameEvent gameEvent)
		{
			await _hubContext.Clients.All.SendAsync("NewEventAdded", gameEvent);
		}

		public async Task SendGameEventToTeamAsync(GameEvent gameEvent, string teamName)
		{
			await _hubContext.Clients.Group(teamName).SendAsync("NewEventAdded", gameEvent);
		}
	}
}
