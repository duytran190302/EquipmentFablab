using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fablab.Models.Domain
{
	public class Borrow
	{
		public string BorrowId { get; set; }
		public DateTime BorrowedDate { get; set; }

		public DateTime ReturnedDate { get; set; }

		public string Borrower { get; set; }
		public string Reason { get; set; }
		public bool OnSide { get; set; }

		public Project Project { get; set; }
		public ICollection<EquipmentBorrow> equipmentBorrows { get; set; }
	}
}
