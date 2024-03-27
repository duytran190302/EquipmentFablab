using Fablab.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Fablab.Models.DTO
{
	public class SearchEquipmentDTO
	{
		public string EquipmentId { get; set; } = string.Empty;
		public string EquipmentName { get; set; } = string.Empty;
		public DateTime YearOfSupply { get; set; }
		public string CodeOfManager { get; set; } = string.Empty;
		//
		public Location Location { get; set; }
		public Supplier Supplier { get; set; }
		[EnumDataType(typeof(EquipmentStatus))]
		public EquipmentStatus Status { get; set; }
		public EquipmentType EquipmentType { get; set; }
	}
}
