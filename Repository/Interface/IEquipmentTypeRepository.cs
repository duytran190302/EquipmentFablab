using Fablab.Models.Domain;
using Fablab.Models.DTO;
using Fablab.Models.DTO.EquipmentTypeDTO;

namespace Fablab.Repository.Interface
{
	public interface IEquipmentTypeRepository: IRepository<EquipmentType>
	{
		Task<EquipmentType> UpdateAsync(EquipmentType entity);
		Task<EquipmentType_Post> PostEquipmentTypeAsync(EquipmentType_Post entity);
		Task<EquipmentType_Put> PutEquipmentTypeAsync(EquipmentType_Put entity);
		Task<EquipmentTypeInf> InfEquipmentTypeAsync(string equipmentTypeId);
		Task<List<EquipmentType>> SearchEquipmenTypeAsync();
	} 
}
