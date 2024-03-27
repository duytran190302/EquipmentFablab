using System.ComponentModel.DataAnnotations.Schema;

namespace Fablab.Models.Domain
{
	public class Picture
	{

		[Column(TypeName = "VARBINARY(MAX)")]
		public byte[] FileData { get; set; }
		public Guid PictureId { get; set; }

		public string EquipmentTypeId { get; set; }
		public EquipmentType EquipmentType { get; set; }

	}
}
