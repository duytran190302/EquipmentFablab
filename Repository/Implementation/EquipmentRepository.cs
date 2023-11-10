using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Models.DTO;
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
			var equip = await _db.Equipment.Where(x => x.EquipmentName == name)
				.Include(x => x.Supplier).Include(x => x.Location).Include(x => x.EquipmentType)
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

		public async Task<List<Equipment>> SearchEquipmentAsync()
		{
			var query = await _db.Equipment.Include(x => x.Supplier)
				.Include(x => x.Location).
				Include(x => x.EquipmentType)
				.Include(x=>x.Borrows)
				.ToListAsync();

			return query;
		}

		public async Task<Equipment> PostEquipmentAsync(PostEquipmentDTO entity)
		{
			//var supplier = await _db.Suppliers.FirstOrDefaultAsync(s => s.SupplierName == entity.EquipmentName);
			var equipment = new Equipment()
			{
				EquipmentId=entity.EquipmentId,
				EquipmentName=entity.EquipmentName,
				YearOfSupply=entity.YearOfSupply,
				CodeOfManager=entity.CodeOfManager,
				Status=entity.Status,
				Supplier= _db.Suppliers.FirstOrDefault(e => e.SupplierName == entity.SupplierName),
				Location = _db.Location.FirstOrDefault(e=>e.LocationId==entity.LocationId),
				EquipmentType= _db.EquipmentTypes.FirstOrDefault(e=>e.EquipmentTypeId==entity.EquipmentTypeId),
			};
			_db.Attach(equipment);
			_db.Equipment.Add(equipment);
			await _db.SaveChangesAsync();
			return equipment;
		}
	}
}
