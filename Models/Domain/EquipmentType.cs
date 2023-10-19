using System.ComponentModel.DataAnnotations;

namespace Fablab.Models.Domain
{
	public class EquipmentType
	{
		public string EquipmentTypeId { get; set; }
		public string Picture { get; set; }
		public string EquipmentTypeName { get; set; }
		//
		[EnumDataType(typeof(Category))]
		public Category Category { get; set; }

	}
}
