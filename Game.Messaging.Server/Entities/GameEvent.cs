namespace Game.Messaging.Server.Entities
{
	public class GameEvent
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime StartsAt { get; set; }
		public DateTime ExpiresAt { get; set; }
		public int EventType { get; set; }
	}
}
