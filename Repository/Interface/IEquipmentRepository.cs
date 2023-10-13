using Fablab.Models.Domain;

namespace Fablab.Repository.Interface
{
	public interface IEquipmentRepository : IRepository<Equipment>
	{
		Task<Equipment> UpdateAsync(Equipment entity);
		Task<List<Equipment>> GetEquipmentByNameAsync(string name);
		Task<List<Equipment>> GetAllEquipmentAsync(int pageSize = 0, int pageNumber = 1);

	}
}
