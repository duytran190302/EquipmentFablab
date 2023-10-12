using Fablab.Models.Domain;

namespace Fablab.Models.DTO
{
	public class BorrowDTO
	{
		public DateTime BorrowedDate { get; set; }
		public DateTime ReturnedDate { get; set; }
		public string Borrower { get; set; }
		public string Reason { get; set; }
		//
		public Project ResearchTopic { get; set; }
		public ICollection<Equipment> Equipment { get; set; }
	}
}
