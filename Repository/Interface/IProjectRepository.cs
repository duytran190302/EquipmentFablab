using Fablab.Models.Domain;
using Fablab.Models.DTO;

namespace Fablab.Repository.Interface
{
	public interface IProjectRepository: IRepository<Project>
	{
		Task<List<Project>> SearchProjectAsync();
		Task<Project> UpdateAsync(Project entity);
		Task<List<Project>> GetProjectByNameAsync(string name);
		Task<List<Project>> GetAllProjectAsync(int pageSize = 0, int pageNumber = 1);
	}
}
