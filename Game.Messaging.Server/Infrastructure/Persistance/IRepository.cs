using Ardalis.Specification;
using System.Linq.Expressions;

namespace Game.Messaging.Server.Infrastructure.Persistance
{
	public interface IRepository<T> : IRepositoryBase<T> where T : class
	{
		Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellation = default);
	}
}
