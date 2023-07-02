using Game.Messaging.Server.Application.Common.Paging;

namespace Game.Messaging.Server.Application.GameOffers.Specifications.Filters
{
	public record GameOffersFilter : PagingFilter
	{
		public string? Name { get; init; }
		public DateTime? StartsBefore { get; init; }
		public DateTime? StartsAfter { get; init; }
		public DateTime? ExpiresBefore { get; init; }
		public DateTime? ExpiresAfter { get; set; }
		public int? OfferType { get; init; }
	}
}

