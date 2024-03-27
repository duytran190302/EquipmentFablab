using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace Fablab.Repository.Interface
{
	public interface IRepos<T, Key> where T : class
	{
		Task<T> GetByIdAsync(Key id);
		Task<IEnumerable<T>> GetAllAsync();
		IEnumerable<T> Find(Expression<Func<T, bool>> expression);
		void Add(T entity);
		void AddRange(IEnumerable<T> entities);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
	}
}
