using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Fablab.Repository.Implementation
{
	public class ProjectRepository : Repository<Project>, IProjectRepository
	{
		private readonly DataContext _db;
		public ProjectRepository(DataContext db) : base(db)
		{
			_db = db;
		}

		public async Task<Project> UpdateAsync(Project entity)
		{
			_db.Project.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
		public async Task<List<Project>> GetProjectByNameAsync(string name)
		{
			var equip = await _db.Project.Where(x => x.ProjectName == name)
				.Include(x => x.Borrows)
				.ToListAsync();
			return equip;
		}

		public async Task<List<Project>> GetAllProjectAsync(int pageSize = 0, int pageNumber = 1)
		{
			if (pageSize > 0)
			{
				if (pageSize > 100)
				{
					pageSize = 100;
				}
				var query = await _db.Project.Include(x => x.Borrows)
					.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
				return query;
			}

			return null;
		}
	}
}
