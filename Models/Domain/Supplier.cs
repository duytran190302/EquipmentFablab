﻿using System.ComponentModel.DataAnnotations;

namespace Fablab.Models.Domain
{
	public class Supplier
	{
		[Key]
		public string SupplierName { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		//
		//public List<Equipment> Equipments { get; set;}
	}
}
