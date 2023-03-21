using Backups.Backup;
using Backups.Repository;

namespace Backups.Extra.BackupExtra;

public interface IBackupTaskExtra : IBackupTask
{
    void RollBack(RestorePoint restorePoint);
    void RollBack(RestorePoint restorePoint, IRepository differentLocation);
}
