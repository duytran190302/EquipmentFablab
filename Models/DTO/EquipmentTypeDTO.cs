using Fablab.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Fablab.Models.DTO
{
	public class EquipmentTypeDTO
	{
		public string EquipmentTypeId { get; set; }
		public string Picture { get; set; }
		public string EquipmentTypeName { get; set; }
		//
		[EnumDataType(typeof(Category))]
		public Category Category { get; set; }
	}
}
