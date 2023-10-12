using Fablab.Models.Domain;

namespace Fablab.Models.DTO
{
	public class SupplierDTO
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		//
		public ICollection<Equipment> Equipments { get; set; }
	}
}
