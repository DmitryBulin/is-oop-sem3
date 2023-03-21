using Backups.Repository;

namespace Backups.Storage;

public interface IStorage
{
    IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects();
    void Clear();
}
