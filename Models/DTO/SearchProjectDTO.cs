using Fablab.Models.Domain;

namespace Fablab.Models.DTO
{
	public class SearchProjectDTO
	{
		public string ProjectName { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Description { get; set; }
		public bool Approved { get; set; }
		public List<string> equipmentName { get; set; }

	}
}
