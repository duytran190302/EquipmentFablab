using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Models.DTO.ProjectFolder;
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
				var query = await _db.Project.Include(x => x.Borrows).Include(x=>x.Borrows)
					.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
				return query;
			}

			return null;
		}


		public async Task<List<Project>> SearchProjectAsync()
		{
			var query = await _db.Project.Include(x => x.Borrows).Include(x=>x.Equipments).ToListAsync();

			return query;
		}

		public async Task<PostProjectDTO2> PostProjectAsync(PostProjectDTO2 entity)
		{
			var Equipment = _db.Equipment.Where(x => entity.Equipments.Contains(x.EquipmentId)).ToList();

			// them phan doi Project cua cac equipment
			var project = new Project()
			{
				ProjectName = entity.ProjectName,
				StartDate = entity.StartDate,
				EndDate = entity.EndDate,
				Description = entity.Description,
				Approved = false,
				///Equipments= new List<Equipment> ()
				Equipments = Equipment,
			};
			/// _db.Attach(borrow); 
			//var dbBorrow = _db.Borrow.Include(x=>x.Equipments).First();


			await _db.Project.AddAsync(project);
			await _db.SaveChangesAsync();


			return entity;
		}

		public Task<Project> EndProjectAsync(EndProjectDTO endProjectDTO)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Equipment>> SearchEquipmentAsync(string equipmentName)
		{
			var query =  _db.Project.Include(x => x.Equipments).Where(x=>x.ProjectName==equipmentName).FirstOrDefault();
			var a = query.Equipments.ToList();
			return a;
		}
	}
}
