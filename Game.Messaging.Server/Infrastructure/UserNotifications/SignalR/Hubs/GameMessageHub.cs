using Microsoft.AspNetCore.SignalR;

namespace Game.Messaging.Server.Infrastructure.UserNotifications.SignalR.Hubs
{
	public class GameMessageHub : Hub
	{
		public override Task OnConnectedAsync()
		{
			var userTeam = Context.User.Claims.FirstOrDefault(x => x.Type == Constants.Claims.TeamClaim)?.Value;
			if (!string.IsNullOrEmpty(userTeam))
			{
				Groups.AddToGroupAsync(Context.ConnectionId, userTeam);
			}

			return base.OnConnectedAsync();
		}
	}
}
