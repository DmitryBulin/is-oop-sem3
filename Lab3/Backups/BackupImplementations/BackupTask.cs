using Backups.Archiver;
using Backups.Exceptions;
using Backups.Repository;
using Backups.Storage;
using Backups.StorageAlgorithm;

namespace Backups.Backup;

public class BackupTask : IBackupTask
{
    private readonly IRepository _repository;
    private readonly IStorageAlgorithm _algorithm;
    private readonly IArchiver _archiver;
    private readonly IBackup _backup;
    private readonly List<IBackupObject> _backupObjects = new List<IBackupObject>();

    public BackupTask(IRepository repository, IStorageAlgorithm algorithm, IArchiver archiver, IBackup backup)
    {
        _repository = repository;
        _algorithm = algorithm;
        _archiver = archiver;
        _backup = backup;
    }

    public IReadOnlyList<IBackupObject> BackupObjects => _backupObjects;

    public void AddObject(IBackupObject backupObject)
    {
        if (_backupObjects.Contains(backupObject))
        {
            throw BackupException.BackupObjectDuplication(backupObject);
        }

        _backupObjects.Add(backupObject);
    }

    public void RemoveObject(IBackupObject backupObject)
    {
        if (!_backupObjects.Remove(backupObject))
        {
            throw BackupException.AbsentBackupObject(backupObject);
        }
    }

    public RestorePoint CreateRestorePoint()
    {
        IStorage storage = _algorithm.StoreData(BackupObjects, _repository, _archiver);
        var restorePoint = new RestorePoint(DateTime.UtcNow, storage, BackupObjects.ToList());
        _backup.AddRestorePoint(restorePoint);
        return restorePoint;
    }
}
