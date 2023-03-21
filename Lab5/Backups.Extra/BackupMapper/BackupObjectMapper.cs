using Backups.Backup;
using Backups.Repository;

namespace Backups.Extra.BackupMapper;

public class BackupObjectMapper : IBackupObjectMapper
{
    private readonly Dictionary<IBackupObject, IRepository> _repositoryMap = new Dictionary<IBackupObject, IRepository>();

    public IBackupObject CreateBackupObject(IRepository repository, string localPath)
    {
        IBackupObject result = new BackupObject(repository, localPath);
        _repositoryMap.Add(result, repository);
        return result;
    }

    public IRepository GetRepository(IBackupObject backupObject)
    {
        IRepository? result = _repositoryMap.GetValueOrDefault(backupObject);
        if (result is null)
        {
            throw new Exception();
        }

        return result;
    }
}
