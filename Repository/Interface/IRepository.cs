﻿using System.Linq.Expressions;

namespace Fablab.Repository.Interface
{
	public interface IRepository<T> where T : class
	{
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
			int pageSize = 0, int pageNumber = 1);
		Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);
		Task CreateAsync(T entity);
		Task CreateNotracking(T entity);
		Task RemoveAsync(T entity);
		Task SaveAsync();
	}
}
