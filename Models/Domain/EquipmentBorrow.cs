namespace Fablab.Models.Domain
{
	public class EquipmentBorrow
	{
		public string BorrowId { get; set; }
		public Borrow Borrow { get; set; }


		public string EquipmentId { get; set; }
		public Equipment Equipment { get; set; }
	}
}
