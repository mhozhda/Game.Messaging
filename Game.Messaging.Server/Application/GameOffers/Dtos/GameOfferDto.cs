using Game.Messaging.Server.Application.Common.Mapping;
using Game.Messaging.Server.Entities;

namespace Game.Messaging.Server.Application.GameOffers.Dtos
{
	public class GameOfferDto : IMapFrom<GameOffer>
	{
		public int Id { get; set; }
		public DateTime StartsAt { get; set; }
		public DateTime ExpiresAt { get; set; }
		public int OfferType { get; set; }
	}
}
