using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Repository.Interface;

namespace Fablab.Repository.Implementation
{
	public class EquipmentTypeRepository : Repository<EquipmentType>, IEquipmentTypeRepository
	{
		private readonly DataContext _db;
		public EquipmentTypeRepository(DataContext db) : base(db)
		{
			_db = db;
		}

		public async Task<EquipmentType> UpdateAsync(EquipmentType entity)
		{
			_db.EquipmentTypes.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
