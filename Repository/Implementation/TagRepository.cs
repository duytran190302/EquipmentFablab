using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Repository.Interface;

namespace Fablab.Repository.Implementation
{
	public class TagRepository : Repository<Tag>, ITagRepository
	{
		private readonly DataContext _db;
		public TagRepository(DataContext db) : base(db)
		{
			_db = db;
		}

		public async Task<Tag> UpdateAsync(Tag entity)
		{
			_db.Tag.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
