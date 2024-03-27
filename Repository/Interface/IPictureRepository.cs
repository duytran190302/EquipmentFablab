using Fablab.Models.Domain;
using Fablab.Models.DTO.SpecificationDTO;

namespace Fablab.Repository.Interface
{
	public interface IPictureRepository : IRepository<Picture>
	{
		Task<Picture> UpdateAsync(Picture entity);
		Task<List<Picture>> GetPictureAsync(string equipmentTypeId);
		Task<Picture> PostPictureAsync(Picture picture);
		Task<Picture> PutPictureAsync(Picture picture);
		Task<string> DeletePictureAsync( string equipmentTypeId);
	}
}
