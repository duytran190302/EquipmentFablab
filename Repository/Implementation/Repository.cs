﻿using Fablab.Data;
using Fablab.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fablab.Repository.Implementation
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly DataContext _db;
		internal DbSet<T> dbSet;
		public Repository(DataContext db)
		{
			_db = db;
			//_db.VillaNumbers.Include(u => u.Villa).ToList();
			this.dbSet = _db.Set<T>();
		}

		public async Task CreateAsync(T entity)
		{
			await dbSet.AddAsync(entity);
			await SaveAsync();
		}

		//"Villa,VillaSpecial"
		public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true )
		{
			IQueryable<T> query = dbSet;
			if (!tracked)
			{
				query = query.AsNoTracking();
			}
			if (filter != null)
			{
				query = query.Where(filter);
			}

			
			return await query.FirstOrDefaultAsync();
		}

		public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
			int pageSize = 0, int pageNumber = 1)
		{
			IQueryable<T> query = dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (pageSize > 0)
			{
				if (pageSize > 100)
				{
					pageSize = 100;
				}
				//skip0.take(5)
				//page number- 2     || page size -5
				//skip(5*(1)) take(5)
				query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
			}
			
			return await query.ToListAsync();
		}

		public async Task RemoveAsync(T entity)
		{
			dbSet.Remove(entity);
			await SaveAsync();
		}

		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}

		public async Task CreateNotracking(T entity)
		{

			await dbSet.AddAsync(entity);

			await SaveAsync();
			//var entry = _db.Entry(entity);
			//entry.State = EntityState.Detached;
		}
	}
}
