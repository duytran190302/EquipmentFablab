using Fablab.Models.Domain;
using Fablab.Models.DTO;
using Fablab.Models.DTO.EquipmentTypeDTO;
using Fablab.Models.DTO.ProjectFolder;

namespace Fablab.Repository.Interface
{
	public interface IProjectRepository: IRepository<Project>
	{
		Task<List<Project>> SearchProjectAsync();
		Task<List<Equipment>> SearchEquipmentAsync(string equipmentName);
		Task<Project> UpdateAsync(Project entity);
		Task<List<Project>> GetProjectByNameAsync(string name);
		Task<List<Project>> GetAllProjectAsync(int pageSize = 0, int pageNumber = 1);
		Task<PostProjectDTO2> PostProjectAsync(PostProjectDTO2 entity);

		Task<Project> EndProjectAsync(EndProjectDTO endProjectDTO);
	}
}
