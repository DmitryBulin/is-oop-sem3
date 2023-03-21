namespace Backups.Repository;

public interface IFile : IRepositoryObject
{
    Stream Open();
}
