using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Fablab.Models.Domain
{
	public enum Category
	{
		[Display(Name = "Mechanical")]
		[Description("Mechanical")]
		Mechanical=1 ,

		[Description("IoT_robotics")]
		[Display(Name = "IoT_robotics")]
		IoT_robotics=2 ,

		[Description("UN")]
		[Display(Name = "Automation")]
		Automation=3
	}
}
