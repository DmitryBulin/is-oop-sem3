using Backups.Backup;
using Backups.Repository;

namespace Backups.Extra.BackupExtra;

public class BackupObjectBridge : IBackupObject
{
    private readonly IRepositoryObject _targetRepositryObject;

    public BackupObjectBridge(IRepositoryObject targetRepositryObject)
    {
        _targetRepositryObject = targetRepositryObject;
    }

    public IRepositoryObject GetCorrespondingRepositoryObject() => _targetRepositryObject;
}
