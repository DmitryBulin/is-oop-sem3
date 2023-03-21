namespace Backups.Backup;

public interface IBackup
{
    IReadOnlyList<RestorePoint> RestorePoints { get; }

    void AddRestorePoint(RestorePoint restorePoint);
}