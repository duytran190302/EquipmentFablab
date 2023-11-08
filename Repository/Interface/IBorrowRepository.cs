using Fablab.Models.Domain;
using Fablab.Models.DTO;
using System.Collections;

namespace Fablab.Repository.Interface
{
	public interface IBorrowRepository: IRepository<Borrow>
	{
		Task<List<Borrow>> SearchBorrowAsync();
		Task<Borrow> UpdateAsync(Borrow entity);
		Task<Borrow> PostBorrowAsync(PostBorrowDTO entity);
		Task ChangeEquipmentOfBorrowAsync(Borrow entity, bool active);
		Task<List<Borrow>> GetAllBorrowAsync(int pageSize = 0, int pageNumber = 1);
		Task<Borrow> GetBorrowByNameAsync(string name);

	}
}
