using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Repository.Interface;

namespace Fablab.Repository.Implementation
{
	public class SupplierRepository : Repository<Supplier>, ISupplierRepository
	{
		private readonly DataContext _db;
		public SupplierRepository(DataContext db) : base(db)
		{
			_db = db;
		}

		public async Task<Supplier> UpdateAsync(Supplier entity)
		{
			_db.Supplier.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
