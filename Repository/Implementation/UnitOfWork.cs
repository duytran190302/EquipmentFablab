using Fablab.Data;
using Fablab.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Fablab.Repository.Implementation
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DataContext _db;
		public UnitOfWork(DataContext db)
		{
			_db = db;
			EquipmentTypeUnitOfWork = new EquipmentTypeUnitOfWork();
			pictureRepos= new PictureRepos( db);
		}

		public IEquipmentTypeUnitOfWork EquipmentTypeUnitOfWork { get; private set; }
		public IPictureRepos pictureRepos { get; private set; }

		public async Task<int> CompleteAsync()
		{
			return await _db.SaveChangesAsync();
		}
		public void Dispose()
		{
			_db.Dispose();
		}
	}
}
