namespace Fablab.Models.DTO.EquipmentTypeDTO
{
	public class EquipmentTypeInf
	{
		public List<Pic> Pics  { get; set; }
		public List<Spec> Specs {  get; set; }
	}

	public class Pic
	{
		public byte[] FileData { get; set; }
	}
	public class Spec
	{
		public string Name { get; set; }
		public string Value { get; set; }
		public string Unit { get; set; }
	}
}
