using Fablab.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Fablab.Models.DTO.EquipmentTypeDTO
{
	public class EquipmentType_Put
	{
		public string EquipmentTypeId { get; set; }
		public string EquipmentTypeName { get; set; }
		public string Description { get; set; }
		//
		[EnumDataType(typeof(Category))]
		public Category Category { get; set; }
		public List<string> Tags { get; set; }
	}
}
