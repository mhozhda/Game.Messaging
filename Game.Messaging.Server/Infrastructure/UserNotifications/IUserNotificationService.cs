using Game.Messaging.Server.Entities;

namespace Game.Messaging.Server.Infrastructure.UserNotifications
{
	public interface IUserNotificationService
	{
		Task SendGameOfferToAllAsync(GameOffer offer);
		Task SendGameOfferToTeamAsync(GameOffer offer, string teamName);
		Task SendGameEventToAllAsync(GameEvent gameEvent);
		Task SendGameEventToTeamAsync(GameEvent gameEvent, string teamName);
	}
}
