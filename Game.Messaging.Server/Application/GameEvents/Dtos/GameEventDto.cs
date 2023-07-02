using Game.Messaging.Server.Application.Common.Mapping;
using Game.Messaging.Server.Entities;

namespace Game.Messaging.Server.Application.GameEvents.Dtos
{
	public class GameEventDto : IMapFrom<GameEvent>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime StartsAt { get; set; }
		public DateTime ExpiresAt { get; set; }
		public int EventType { get; set; }
	}
}
