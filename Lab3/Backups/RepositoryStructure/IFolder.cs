namespace Backups.Repository;

public interface IFolder : IRepositoryObject
{
    IReadOnlyCollection<IRepositoryObject> SubObjects { get; }
}
