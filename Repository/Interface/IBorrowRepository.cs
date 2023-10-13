using Fablab.Models.Domain;
using System.Collections;

namespace Fablab.Repository.Interface
{
	public interface IBorrowRepository: IRepository<Borrow>
	{
		Task<Borrow> UpdateAsync(Borrow entity);

	}
}
