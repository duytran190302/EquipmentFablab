namespace Fablab.Models.DTO
{
	public class BorrowDTO
	{
		public string BorrowId { get; set; } = string.Empty;
		public DateTime BorrowedDate { get; set; }

		public DateTime ReturnedDate { get; set; }
		public DateTime? RealReturnedDate { get; set; }

		public string Borrower { get; set; }
		public string Reason { get; set; } = string.Empty;
		public bool OnSide { get; set; }
	}
}
