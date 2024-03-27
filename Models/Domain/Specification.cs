namespace Fablab.Models.Domain
{
	public class Specification
	{
		public string Name { get; set; }
		public string Value { get; set; }
		public string Unit { get; set; }

		public string EquipmentTypeId { get; set; }
		public EquipmentType EquipmentType { get; set; }

	}
}
