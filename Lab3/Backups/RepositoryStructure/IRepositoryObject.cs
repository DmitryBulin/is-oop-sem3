using Backups.Visitor;

namespace Backups.Repository;

public interface IRepositoryObject
{
    string Name { get; }
    void Accept(IRepositoryObjectVisitor visitor);
}
