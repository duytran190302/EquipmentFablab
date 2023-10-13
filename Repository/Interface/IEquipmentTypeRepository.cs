using Fablab.Models.Domain;

namespace Fablab.Repository.Interface
{
	public interface IEquipmentTypeRepository: IRepository<EquipmentType>
	{
		Task<EquipmentType> UpdateAsync(EquipmentType entity);
	}
}
