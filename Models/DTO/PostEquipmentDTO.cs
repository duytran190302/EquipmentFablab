using Fablab.Models.Domain;

namespace Fablab.Models.DTO
{
	public class PostEquipmentDTO
	{
		public string EquipmentId { get; set; }
		public string EquipmentName { get; set; }
		public DateTime YearOfSupply { get; set; }
		public string CodeOfManager { get; set; }
		public EquipmentStatus Status { get; set; }
		///
		public string LocationId { get; set; }
		public string SupplierName { get; set; }
		public string EquipmentTypeId { get; set; }
		
	}
}
