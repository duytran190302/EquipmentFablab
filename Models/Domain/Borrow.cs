﻿namespace Fablab.Models.Domain
{
	public class Borrow
	{
		public DateTime BorrowedDate { get; set; }
		public DateTime ReturnedDate { get; set; }
		public string Borrower { get; set; }
		public string Reason { get; set; }
		//
		public ResearchTopic ResearchTopic { get; set; }
		public ICollection<Equipment> Equipment { get; set; }
	}
}
