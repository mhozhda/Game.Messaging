namespace Game.Messaging.Server.Application.Common.Paging
{
	public abstract record PagingFilter
	{
		public int Page { get; init; }
		public int PageSize { get; init; }
		public bool IsPagingEnabled => Page > 0 && PageSize > 0;
	}
}
