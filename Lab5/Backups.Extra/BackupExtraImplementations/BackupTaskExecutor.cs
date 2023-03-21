using Backups.Archiver;
using Backups.Backup;
using Backups.Repository;
using Backups.Storage;
using Backups.StorageAlgorithm;

namespace Backups.Extra.BackupExtra;

public class BackupTaskExecutor : IBackupTaskExecutor
{
    private readonly IRepository _repository;
    private readonly IStorageAlgorithm _algorithm;
    private readonly IArchiver _archiver;

    public BackupTaskExecutor(IRepository repository, IStorageAlgorithm algorithm, IArchiver archiver)
    {
        _repository = repository;
        _algorithm = algorithm;
        _archiver = archiver;
    }

    public RestorePoint CreateRestorePoint(DateTime creationTime, IReadOnlyList<IBackupObject> backupObjects)
    {
        IStorage storage = _algorithm.StoreData(backupObjects, _repository, _archiver);
        var restorePoint = new RestorePoint(creationTime, storage, backupObjects.ToList());
        return restorePoint;
    }
}
