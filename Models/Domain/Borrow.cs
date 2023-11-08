using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fablab.Models.Domain
{
	public class Borrow
	{
		public string BorrowId { get; set; } = string.Empty;
		public DateTime BorrowedDate { get; set; }

		public DateTime ReturnedDate { get; set; }
		public DateTime? RealReturnedDate { get; set; }

		public string Borrower { get; set; }
		public string Reason { get; set; } = string.Empty;
		public bool OnSide { get; set; }

		public Project Project { get; set; }
		public List<Equipment> Equipments { get; set; }
	}
}
