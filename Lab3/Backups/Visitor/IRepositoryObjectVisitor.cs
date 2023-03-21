using Backups.Repository;

namespace Backups.Visitor;

public interface IRepositoryObjectVisitor
{
    void Visit(IFile file);
    void Visit(IFolder folder);
}
