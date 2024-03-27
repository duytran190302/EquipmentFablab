namespace Fablab.Models.DTO.ProjectFolder
{
	public class PostProjectDTO2
	{
		public string ProjectName { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Description { get; set; }
		public List<string> Equipments { get; set; }
	}
}
