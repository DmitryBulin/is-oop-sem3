using Backups.Backup;

namespace Backups.Exceptions;

public class BackupException : BackupsException
{
    private BackupException(string message)
        : base(message)
    {
    }

    public static BackupException RestorePointDuplication(Guid id)
    {
        return new BackupException($"Tried to add duplication of restore point {id} to backup");
    }

    public static BackupException BackupObjectDuplication(IBackupObject backupObject)
    {
        return new BackupException($"Tried to add duplication of backup object {backupObject} to backup task");
    }

    public static BackupException AbsentBackupObject(IBackupObject backupObject)
    {
        return new BackupException($"Failed to remove backup object {backupObject} from backup task");
    }
}
