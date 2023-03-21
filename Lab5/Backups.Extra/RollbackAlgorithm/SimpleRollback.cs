using Backups.Repository;
using Backups.Visitor;

namespace Backups.Extra.RollbackAlgorithm;

public class SimpleRollback : IRollbackAlgorithm
{
    public void Restore(IRepositoryObject repositoryObject, IRepository repository)
    {
        IRepositoryObjectVisitor rollbackVisitor = new RollbackVisitor(repository);
        repositoryObject.Accept(rollbackVisitor);
    }
}
