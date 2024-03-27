using Fablab.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Fablab.Models.DTO.EquipmentTypeDTO
{
	public class EquipmentTypePostAll
	{
		public string EquipmentTypeId { get; set; }
		public string EquipmentTypeName { get; set; }
		public string Description { get; set; }
		//
		[EnumDataType(typeof(Category))]
		public Category Category { get; set; }
		public List<string> Tags { get; set; }
		public List<PictureOfPostAll> Pictures { get; set; }
		public List<SpecificationOfPostAll> Specifications { get; set; }
	}
	public class PictureOfPostAll
	{
		public string FileData { get; set; }
	}
	public class SpecificationOfPostAll
	{
		public string Name { get; set; }
		public string Value { get; set; }
		public string Unit { get; set; }
	}
}
