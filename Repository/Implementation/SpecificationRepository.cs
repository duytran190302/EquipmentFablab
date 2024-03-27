using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Models.DTO.SpecificationDTO;
using Fablab.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Fablab.Repository.Implementation
{
	public class SpecificationRepository : Repository<Specification>, ISpecificationRepository
	{
		private readonly DataContext _db;
		public SpecificationRepository(DataContext db) : base(db)
		{
			_db = db;
		}

		public async Task<Specification> DeleteSpecificationAsync(string name, string equipmentTypeId)
		{
			var specification = new Specification();
			specification = _db.Specification.Where(x=>x.Name==name).Where(x=>x.EquipmentType.EquipmentTypeId== equipmentTypeId).First();
			_db.Specification.Remove(specification);
			await _db.SaveChangesAsync();
			return specification;
		}

		public async Task<List<Specification>> GetSpecificationAsync(string equipmentTypeId)
		{
			var query = await _db.Specification.Include(x=>x.EquipmentType).Where(x=>x.EquipmentType.EquipmentTypeId==equipmentTypeId).AsNoTracking().ToListAsync();

			return query;
		}

		public async Task<Specification> PostSpecificationAsync(PostSpecificationDTO postSpecificationDTO)
		{
			var specification = new Specification()
			{
				Name = postSpecificationDTO.Name,
				Value = postSpecificationDTO.Value,
				Unit= postSpecificationDTO.Unit,
				EquipmentTypeId=postSpecificationDTO.EquipmentTypeId,
				//EquipmentType= _db.EquipmentType.FirstOrDefault(x => x.EquipmentTypeId ==postSpecificationDTO.EquipmentTypeId)
			};
			_db.Attach(specification);
			_db.Specification.Add(specification);
			await _db.SaveChangesAsync();
			return specification;
		}

		public async Task<Specification> PutSpecificationAsync(PostSpecificationDTO postSpecificationDTO)
		{
			var specification = new Specification()
			{
				Name = postSpecificationDTO.Name,
				Value = postSpecificationDTO.Value,
				Unit = postSpecificationDTO.Unit,
				EquipmentTypeId = postSpecificationDTO.EquipmentTypeId,
				//EquipmentType = _db.EquipmentType.FirstOrDefault(x => x.EquipmentTypeId == postSpecificationDTO.EquipmentTypeId)
			};
			_db.Attach(specification);
			_db.Specification.Update(specification);
			await _db.SaveChangesAsync();
			return specification;
		}

		public async Task<Specification> UpdateAsync(Specification entity)
		{
			_db.Specification.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
