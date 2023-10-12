using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Repository.Interface;

namespace Fablab.Repository.Implementation
{
	public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
	{
		private readonly DataContext _db;
		public EquipmentRepository(DataContext db) : base(db)
		{
			_db = db;
		}

		public async Task<Equipment> UpdateAsync(Equipment entity)
		{
			_db.Equipment.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
