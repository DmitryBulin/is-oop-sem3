using Backups.Exceptions;

namespace Backups.Backup;

public class Backup : IBackup
{
    private readonly List<RestorePoint> _restorePoints = new List<RestorePoint>();
    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        if (_restorePoints.Contains(restorePoint))
        {
            throw BackupException.RestorePointDuplication(restorePoint.Id);
        }

        _restorePoints.Add(restorePoint);
    }
}
