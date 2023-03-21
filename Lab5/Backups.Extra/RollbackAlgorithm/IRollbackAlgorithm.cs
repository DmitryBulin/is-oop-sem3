using Backups.Repository;

namespace Backups.Extra.RollbackAlgorithm;

public interface IRollbackAlgorithm
{
    void Restore(IRepositoryObject repositoryObject, IRepository repository);
}
