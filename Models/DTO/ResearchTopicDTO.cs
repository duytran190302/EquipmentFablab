using Fablab.Models.Domain;

namespace Fablab.Models.DTO
{
	public class ResearchTopicDTO
	{
		public string Name { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Content { get; set; }
		//
		public Borrow Borrow { get; set; }
		public BorrowFromOutside BorrowFromOutside { get; set; }
	}
}
