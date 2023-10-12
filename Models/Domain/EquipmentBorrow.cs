namespace Fablab.Models.Domain
{
	public class EquipmentBorrow
	{
		public Guid BorrowID { get; set; }
		public Borrow Borrow { get; set; }


		public Guid EquipmentId { get; set; }
		public Equipment Equipment { get; set; }
	}
}
