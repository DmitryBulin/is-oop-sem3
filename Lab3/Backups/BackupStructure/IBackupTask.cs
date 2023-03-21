namespace Backups.Backup;

public interface IBackupTask
{
    IReadOnlyList<IBackupObject> BackupObjects { get; }
    void AddObject(IBackupObject backupObject);
    void RemoveObject(IBackupObject backupObject);
    RestorePoint CreateRestorePoint();
}
