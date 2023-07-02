namespace Game.Messaging.Server.Application.Common.Paging
{
	public class PagingResponse<T> where T : class
	{
		public PagingResponse(PagingFilter pagingRequest, List<T> results, int totalCount)
		{
			if (pagingRequest.IsPagingEnabled)
			{
				Page = pagingRequest.Page;
				PageSize = pagingRequest.PageSize;
			}

			Results = results.AsReadOnly();
			TotalCount = totalCount;
		}

		public int? Page { get; }
		public int? PageSize { get; }
		public int TotalCount { get; }
		public IReadOnlyCollection<T> Results { get; }
	}
}
