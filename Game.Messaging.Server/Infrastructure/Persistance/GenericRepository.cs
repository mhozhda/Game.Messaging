using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Game.Messaging.Server.Infrastructure.Persistance
{
	public class GenericRepository<T> : RepositoryBase<T>, IRepository<T> where T : class
	{
		private readonly GameEventsDbContext _dbContext;

		public GenericRepository(GameEventsDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellation = default)
		{
			return _dbContext.Set<T>().AnyAsync(expression, cancellation);
		}
	}
}
