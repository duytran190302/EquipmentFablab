using Fablab.Models.Domain;

namespace Fablab.Repository.Interface
{
	public interface ISupplierRepository: IRepository<Supplier>
	{
		Task<Supplier> UpdateAsync(Supplier entity);
	}
}
