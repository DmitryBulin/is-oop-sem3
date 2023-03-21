using Backups.Repository;
using Backups.Visitor;

namespace Backups.Extra.RollbackAlgorithm;

public class RollbackVisitor : IRepositoryObjectVisitor
{
    private readonly IRepository _repository;

    public RollbackVisitor(IRepository repository)
    {
        _repository = repository;
    }

    public void Visit(IFile file)
    {
        using Stream repositoryStream = _repository.OpenWrite(file.Name);
        using Stream fileStream = file.Open();
        fileStream.CopyTo(repositoryStream);
    }

    public void Visit(IFolder folder)
    {
        foreach (IRepositoryObject subObject in folder.SubObjects)
        {
            subObject.Accept(this);
        }
    }
}
