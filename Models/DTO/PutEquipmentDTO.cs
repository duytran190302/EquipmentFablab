using Fablab.Models.Domain;

namespace Fablab.Models.DTO
{
	public class PutEquipmentDTO
	{
		public string EquipmentId { get; set; }
		public string EquipmentName { get; set; }
		public DateTime YearOfSupply { get; set; }
		public string CodeOfManager { get; set; }
		public EquipmentStatus Status { get; set; }
	}
}
