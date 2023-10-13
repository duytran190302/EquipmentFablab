using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Fablab.Repository.Implementation
{
	public class BorrowRepository : Repository<Borrow>, IBorrowRepository
	{
		private readonly DataContext _db;
		public BorrowRepository(DataContext db) : base(db)
		{
			_db = db;
		}



		public async Task<Borrow> UpdateAsync(Borrow entity)
		{
			_db.Borrow.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
