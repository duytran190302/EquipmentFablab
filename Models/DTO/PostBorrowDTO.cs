using Fablab.Models.Domain;
using System.Collections;

namespace Fablab.Models.DTO
{
	public class PostBorrowDTO
	{
		public string BorrowId { get; set; }
		public DateTime BorrowedDate { get; set; }

		public DateTime ReturnedDate { get; set; }

		public string Borrower { get; set; }
		public string Reason { get; set; }
		public bool OnSide { get; set; }

		public string ProjectName { get; set; }
		public List<string> Equipment { get; set; }
	}
}
