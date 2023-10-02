using Fablab.Models.Domain;

namespace Fablab.Models.DTO
{
	public class EquipmentDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Picture { get; set; }
		public DateTime YearSupply { get; set; }
		public string CodeOfManage { get; set; }
		//
		public Supplier Supplier { get; set; }
		public EquipmentSpecifications Specification { get; set; }
		public Borrow Borrow { get; set; }
		public BorrowFromOutside BorrowFromOutside { get; set; }
		public EquipmentStatus Status { get; set; }
		public EquipmentType EquipmentType { get; set; }
	}
}
