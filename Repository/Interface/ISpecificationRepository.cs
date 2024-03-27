using Fablab.Models.Domain;
using Fablab.Models.DTO.SpecificationDTO;
using System.Xml.Linq;

namespace Fablab.Repository.Interface
{
	public interface ISpecificationRepository : IRepository<Specification>
	{
		Task<Specification> UpdateAsync(Specification entity);
		Task<List<Specification>> GetSpecificationAsync(string equipmentTypeId);
		Task<Specification> PostSpecificationAsync(PostSpecificationDTO postSpecificationDTO );
		Task<Specification> PutSpecificationAsync(PostSpecificationDTO postSpecificationDTO);
		Task<Specification> DeleteSpecificationAsync(string name, string equipmentTypeId);
	} 
}
