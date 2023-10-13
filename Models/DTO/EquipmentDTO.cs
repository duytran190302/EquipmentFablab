using Fablab.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Fablab.Models.DTO
{
	public class EquipmentDTO
	{
		public string EquipmentName { get; set; }
		public DateTime YearOfSupply { get; set; }
		public string CodeOfManager { get; set; }
		//
		public Supplier Supplier { get; set; }
		public Location Location { get; set; }
		[EnumDataType(typeof(EquipmentStatus))]
		public EquipmentStatus Status { get; set; }
		public EquipmentType EquipmentType { get; set; }
	}
}
