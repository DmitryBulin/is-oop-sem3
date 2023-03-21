using Backups.Backup;
using Backups.Extra.BackupExtra;
using Backups.Repository;

namespace Backups.Extra.Logging;

public class BackupTaskExtraLoggerDecorator : IBackupTaskExtra
{
    private readonly IBackupTaskExtra _target;
    private readonly ILogger _logger;

    public BackupTaskExtraLoggerDecorator(IBackupTaskExtra target, ILogger logger)
    {
        _target = target;
        _logger = logger;
    }

    public IReadOnlyList<IBackupObject> BackupObjects => _target.BackupObjects;

    public void AddObject(IBackupObject backupObject)
    {
        _logger.Log($"Add object {backupObject} to task");
        _target.AddObject(backupObject);
        _logger.Log($"Successfully added new object {backupObject} to task");
    }

    public void RemoveObject(IBackupObject backupObject)
    {
        _logger.Log($"Remove object {backupObject} from task");
        _target.RemoveObject(backupObject);
        _logger.Log($"Successfully removed object {backupObject} from task");
    }

    public RestorePoint CreateRestorePoint()
    {
        _logger.Log($"Create new restore point");
        RestorePoint result = _target.CreateRestorePoint();
        _logger.Log($"Successfully created new restore point {result}");
        return result;
    }

    public void RollBack(RestorePoint restorePoint)
    {
        _logger.Log($"Roll back to restore point {restorePoint} in original repository");
        _target.RollBack(restorePoint);
        _logger.Log($"Successfully rolled back to restore point {restorePoint}");
    }

    public void RollBack(RestorePoint restorePoint, IRepository differentLocation)
    {
        _logger.Log($"Roll back to restore point {restorePoint} in repository {differentLocation}");
        _target.RollBack(restorePoint, differentLocation);
        _logger.Log($"Successfully rolled back to restore point {restorePoint}");
    }
}
