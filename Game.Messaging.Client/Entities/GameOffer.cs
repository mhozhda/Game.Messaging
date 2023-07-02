namespace Game.Messaging.Client.Entities
{
	public class GameOffer
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime StartsAt { get; set; }
		public DateTime ExpiresAt { get; set; }
		public int OfferType { get; set; }

		public override string ToString()
		{
			return $"{Name}, Id: {Id}, StartsAt: {StartsAt}, ExpiresAt: {ExpiresAt}, OfferType: {OfferType}";
		}
	}
}
