namespace Fablab.Models.DTO
{
	public class PutBorrowDTO
	{
		public string BorrowId { get; set; }
		public DateTime BorrowedDate { get; set; }

		public DateTime ReturnedDate { get; set; }
		public DateTime? RealReturnedDate { get; set; }

		public string Borrower { get; set; }
		public string Reason { get; set; }
		public bool OnSide { get; set; }
	}
}
