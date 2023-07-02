using Game.Messaging.Server.Application.Common.Paging;

namespace Game.Messaging.Server.Application.GameEvents.Specifications.Filters
{
	public record GameEventsFilter : PagingFilter
	{
		public string? Name { get; set; }
		public DateTime? StartsBefore { get; init; }
		public DateTime? StartsAfter { get; init; }
		public DateTime? ExpiresBefore { get; init; }
		public DateTime? ExpiresAfter { get; set; }
		public int? EventType { get; init; }
	}
}
