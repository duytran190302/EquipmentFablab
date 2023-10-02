namespace Fablab.Models.Domain
{
	public class Supplier
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		//
		public ICollection<Equipment> Equipments { get; set; }
		public ICollection<SupplierAddress> supplierAddresses { get; set; }
	}
}
