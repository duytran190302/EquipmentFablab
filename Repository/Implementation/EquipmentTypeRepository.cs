using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Models.DTO.EquipmentTypeDTO;
using Fablab.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Fablab.Repository.Implementation
{
	public class EquipmentTypeRepository : Repository<EquipmentType>, IEquipmentTypeRepository
	{
		private readonly DataContext _db;
		public EquipmentTypeRepository(DataContext db) : base(db)
		{
			_db = db;
		}

		public async Task<EquipmentTypeInf> InfEquipmentTypeAsync(string equipmentTypeId)
		{
			var pic = _db.Picture.Where(x => x.EquipmentTypeId == equipmentTypeId).ToList();
			var spec = _db.Specification.Where(x => x.EquipmentTypeId == equipmentTypeId);
			EquipmentTypeInf? Inf = new EquipmentTypeInf();
			var Picture = new List<Pic>();
			var Specification = new List<Spec>();
			foreach (var item in pic)
			{
				var picture = new Pic();
				picture.FileData=item.FileData;
				Picture.Add(picture);
			}
			foreach (var item in spec)
			{
				var specification = new Spec();
				specification.Name = item.Name;
				specification.Value = item.Value;
				specification.Unit = item.Unit;
				Specification.Add(specification);
			}

			Inf.Specs = Specification;
			Inf.Pics = Picture;
			return Inf;
		}

		//public Task<EquipmentTypeInf> InfEquipmentTypeAsync(string equipmentTypeId)
		//{

		//}

		public async Task<EquipmentType_Post> PostEquipmentTypeAsync(EquipmentType_Post entity)
		{
			var Tag = _db.Tag.Where(x => entity.Tags.Contains(x.TagId)).ToList();
			var equipmentType = new EquipmentType()
			{
				EquipmentTypeId = entity.EquipmentTypeId,
				EquipmentTypeName = entity.EquipmentTypeName,
				Description = entity.Description,
				Category = entity.Category,

				///Equipments= new List<Equipment> ()
				Tags = Tag,
			};
			/// _db.Attach(borrow); 
			//var dbBorrow = _db.Borrow.Include(x=>x.Equipments).First();
			await _db.EquipmentType.AddAsync(equipmentType);
			await _db.SaveChangesAsync();


			return entity;
		}

		public async Task<EquipmentType_Put> PutEquipmentTypeAsync(EquipmentType_Put entity)
		{
			var Tag = _db.Tag.Where(x => entity.Tags.Contains(x.TagId)).ToList();
			var equipmentType = new EquipmentType()
			{
				EquipmentTypeId = entity.EquipmentTypeId,
				EquipmentTypeName = entity.EquipmentTypeName,
				Description = entity.Description,
				Category = entity.Category,

				///Equipments= new List<Equipment> ()
				Tags = Tag,
			};
			/// _db.Attach(borrow); 
			//var dbBorrow = _db.Borrow.Include(x=>x.Equipments).First();
			 _db.EquipmentType.Update(equipmentType);
			await _db.SaveChangesAsync();


			return entity;
		}

		public async Task<List<EquipmentType>> SearchEquipmenTypeAsync()
		{
			var query = await _db.EquipmentType.Include(x => x.Tags).ToListAsync();

			return query;
		}

		public async Task<EquipmentType> UpdateAsync(EquipmentType entity)
		{
			_db.EquipmentType.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
