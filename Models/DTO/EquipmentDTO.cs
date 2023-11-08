using Fablab.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Fablab.Models.DTO
{
	public class EquipmentDTO
	{
		public string EquipmentId { get; set; }
		public string EquipmentName { get; set; }
		public DateTime YearOfSupply { get; set; }
		public string CodeOfManager { get; set; }
		[EnumDataType(typeof(EquipmentStatus))]
		public EquipmentStatus Status { get; set; }


	}
}
