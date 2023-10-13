using Fablab.Models.Domain;

namespace Fablab.Repository.Interface
{
	public interface ILocationRepository: IRepository<Location>
	{
		Task<Location> UpdateAsync(Location entity);
	}
}
