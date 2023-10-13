using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Repository.Interface;

namespace Fablab.Repository.Implementation
{
	public class LocationRepository : Repository<Location>, ILocationRepository
	{
		private readonly DataContext _db;
		public LocationRepository(DataContext db) : base(db)
		{
			_db = db;
		}

		public async Task<Location> UpdateAsync(Location entity)
		{
			_db.Location.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
