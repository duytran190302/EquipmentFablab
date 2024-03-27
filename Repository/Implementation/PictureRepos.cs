using Fablab.Data;
using Fablab.Models.Domain;
using Fablab.Repository.Interface;

namespace Fablab.Repository.Implementation
{
	public class PictureRepos : Repos<Picture, string>, IPictureRepos
	{
		public PictureRepos(DataContext context) : base(context)
		{
		}
	}
}
