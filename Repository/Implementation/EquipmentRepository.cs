using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
		public async Task<List<Equipment>> GetEquipmentByNameAsync(string name)
		{
			var equip =  await _db.Equipment.Where(x=>x.EquipmentName==name)
				.Include(x=>x.Supplier).Include(x=>x.Location).Include(x=> x.EquipmentType)
				.ToListAsync();
			return equip;
		}

		public async Task<List<Equipment>> GetAllEquipmentAsync(int pageSize = 0, int pageNumber = 1)
		{

			if (pageSize > 0)
			{
				if (pageSize > 100)
				{
					pageSize = 100;
				}
				var query = await _db.Equipment.Include(x => x.Supplier).Include(x => x.Location).Include(x => x.EquipmentType)
					.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
				return query;
			}

			return null;
		}
	}
}
