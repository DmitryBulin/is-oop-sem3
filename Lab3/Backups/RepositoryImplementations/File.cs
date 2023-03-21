using Backups.Visitor;

namespace Backups.Repository;

public class File : IFile
{
    private readonly Func<Stream> _streamFunc;

    public File(Func<Stream> streamFunc, string name)
    {
        _streamFunc = streamFunc;
        Name = name;
    }

    public string Name { get; }

    public void Accept(IRepositoryObjectVisitor visitor)
    {
        visitor.Visit(this);
    }

    public Stream Open() => _streamFunc();
}
