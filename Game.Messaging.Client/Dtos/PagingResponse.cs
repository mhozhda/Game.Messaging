namespace Game.Messaging.Client.Dtos
{
	public class PagingResponse<T> where T : class
	{
		public int? Page { get; set; }
		public int? PageSize { get; set; }
		public int TotalCount { get; set; }
		public List<T> Results { get; set; }
	}
}
