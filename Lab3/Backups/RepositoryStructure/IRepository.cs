namespace Backups.Repository;

public interface IRepository
{
    Stream OpenWrite(string localPath);
    Stream OpenRead(string localPath);
    IRepositoryObject GetRepositoryObject(string localPath);
    void Delete(IRepositoryObject repositoryObject);
}
