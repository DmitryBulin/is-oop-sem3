using Backups.Visitor;

namespace Backups.Repository;

public class Folder : IFolder
{
    private readonly Func<IReadOnlyCollection<IRepositoryObject>> _subItemsFunc;

    public Folder(Func<IReadOnlyCollection<IRepositoryObject>> subItemsFunc, string name)
    {
        _subItemsFunc = subItemsFunc;
        Name = name;
    }

    public IReadOnlyCollection<IRepositoryObject> SubObjects => _subItemsFunc();

    public string Name { get; }

    public void Accept(IRepositoryObjectVisitor visitor)
    {
        visitor.Visit(this);
    }
}
