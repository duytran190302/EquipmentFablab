using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Fablab.Models.Domain
{
	public enum EquipmentStatus
	{
		[Display(Name = "Active")]
		[Description("Active")]
		Active=1,

		[Display(Name = "Inactive")]
		[Description("Inactive")]
		Inactive=2,

		[Display(Name = "Nonfunctional")]
		[Description("Nonfunctional")]
		Nonfunctional=3,

		[Display(Name = "Maintenance")]
		[Description("Maintenance")]
		Maintenance=4
	}
}
