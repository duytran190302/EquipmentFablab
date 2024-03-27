namespace Fablab.Repository.Interface
{
	public interface IUnitOfWork
	{
		IEquipmentTypeUnitOfWork EquipmentTypeUnitOfWork { get; }
		IPictureRepos pictureRepos { get; }
		Task<int> CompleteAsync();
	}
}
