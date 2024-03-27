using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Models.DTO.SpecificationDTO;
using Fablab.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Fablab.Repository.Implementation
{
	public class PictureRepository : Repository<Picture>, IPictureRepository
	{
		private readonly DataContext _db;
		public PictureRepository(DataContext db) : base(db)
		{
			_db = db;
		}

		public async Task<string> DeletePictureAsync(string equipmentTypeId)
		{
			var picture = new List<Picture>();
			picture = _db.Picture.Where(x => x.EquipmentType.EquipmentTypeId == equipmentTypeId).ToList();
			foreach (var item in picture)
			{
				_db.Picture.Remove(item);
			}
			await _db.SaveChangesAsync();
			return equipmentTypeId;
		}



		public async Task<List<Picture>> GetPictureAsync(string equipmentTypeId)
		{
			var query = await _db.Picture.Include(x => x.EquipmentType).Where(x => x.EquipmentType.EquipmentTypeId == equipmentTypeId).AsNoTracking().ToListAsync();

			return query;
		}

		public async Task<Picture> PostPictureAsync(Picture picture)
		{
			var pic = new Picture()
			{
				FileData = picture.FileData,
				EquipmentTypeId = picture.EquipmentTypeId,
				//EquipmentType= _db.EquipmentType.FirstOrDefault(x => x.EquipmentTypeId ==postSpecificationDTO.EquipmentTypeId)
			};
			_db.Attach(pic);
			_db.Picture.Add(pic);
			await _db.SaveChangesAsync();
			return pic;
		}

		public Task<Picture> PutPictureAsync(Picture picture)
		{
			throw new NotImplementedException();
		}

		public async Task<Picture> UpdateAsync(Picture entity)
		{
			_db.Picture.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}

	}
}
