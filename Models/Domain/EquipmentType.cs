using System.ComponentModel.DataAnnotations;

namespace Fablab.Models.Domain
{
	public class EquipmentType
	{
		public string EquipmentTypeId { get; set; }
		public string EquipmentTypeName { get; set; }
		public string Description { get; set; }
		//
		[EnumDataType(typeof(Category))]
		public Category Category { get; set; }

		public List<Picture> Pictures { get; set; }
		public List<Tag> Tags { get; set; }
		public List<Specification> Specifications { get; set; }
	}
}
