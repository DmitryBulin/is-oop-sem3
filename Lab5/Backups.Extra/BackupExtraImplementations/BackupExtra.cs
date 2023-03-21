using Backups.Backup;
using Backups.Extra.CleanAlgorithm;
using Backups.Extra.SelectAlgorithm;

namespace Backups.Extra.BackupExtra;

public class BackupExtra : IBackup
{
    private readonly ISelectAlgorithm _selectAlgorithm;
    private readonly ICleanAlgorithm _cleanAlgorithm;
    private List<RestorePoint> _restorePoints = new List<RestorePoint>();

    public BackupExtra(ISelectAlgorithm selectAlgorithm, ICleanAlgorithm cleanAlgorithm)
    {
        _selectAlgorithm = selectAlgorithm;
        _cleanAlgorithm = cleanAlgorithm;
    }

    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        if (_restorePoints.Contains(restorePoint))
        {
            throw new Exception();
        }

        _restorePoints.Add(restorePoint);
        IReadOnlyList<RestorePoint> blackList = _selectAlgorithm.SelectBlacklist(_restorePoints);

        if (blackList.Count == _restorePoints.Count)
        {
            throw new Exception();
        }

        if (blackList.Count != 0)
        {
            _restorePoints = _cleanAlgorithm.Clean(_restorePoints, blackList).ToList();
        }
    }
}
