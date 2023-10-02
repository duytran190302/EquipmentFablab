namespace Fablab.Models.Domain
{
	public class ResearchTopic
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
