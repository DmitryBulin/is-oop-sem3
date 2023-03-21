using Backups.Storage;

namespace Backups.Backup;

public class RestorePoint
{
    public RestorePoint(DateTime creationTime, IStorage storage, IReadOnlyCollection<IBackupObject> backupObjects)
    {
        CreationTime = creationTime;
        Storage = storage;
        BackupObjects = backupObjects;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public DateTime CreationTime { get; }
    public IStorage Storage { get; }
    public IReadOnlyCollection<IBackupObject> BackupObjects { get; }
}
