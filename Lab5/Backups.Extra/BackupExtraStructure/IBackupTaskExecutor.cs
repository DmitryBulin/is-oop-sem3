using Backups.Backup;

namespace Backups.Extra.BackupExtra;

public interface IBackupTaskExecutor
{
    RestorePoint CreateRestorePoint(DateTime creationTime, IReadOnlyList<IBackupObject> backupObjects);
}