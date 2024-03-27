using Fablab.Models.Domain;

namespace Fablab.Repository.Interface
{
	public interface ITagRepository : IRepository<Tag>
	{
		Task<Tag> UpdateAsync(Tag entity);
	}
}
