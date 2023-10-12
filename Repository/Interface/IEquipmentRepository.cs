using Fablab.Models.Domain;

namespace Fablab.Repository.Interface
{
	public interface IEquipmentRepository : IRepository<Equipment>
	{
		Task<Equipment> UpdateAsync(Equipment entity);
	}
}
