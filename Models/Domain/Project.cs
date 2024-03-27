using System.ComponentModel.DataAnnotations;

namespace Fablab.Models.Domain
{
	public class Project
	{
		public string ProjectName { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime RealEndDate { get; set; }
		public string Description { get; set; }
		public bool Approved { get; set; }
		//
		public List<Borrow> Borrows { get; set; }
		public List<Equipment> Equipments { get; set; }


	}
}
