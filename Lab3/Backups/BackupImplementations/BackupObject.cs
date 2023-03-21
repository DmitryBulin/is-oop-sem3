using Backups.Repository;

namespace Backups.Backup;

public class BackupObject : IBackupObject
{
    private readonly IRepository _repository;

    public BackupObject(IRepository repository, string name)
    {
        _repository = repository;
        Name = name;
    }

    public string Name { get; }

    public IRepositoryObject GetCorrespondingRepositoryObject()
    {
        return _repository.GetRepositoryObject(Name);
    }
}
