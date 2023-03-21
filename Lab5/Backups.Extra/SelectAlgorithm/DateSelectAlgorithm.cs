using Backups.Backup;

namespace Backups.Extra.SelectAlgorithm;

public class DateSelectAlgorithm : ISelectAlgorithm
{
    private readonly TimeSpan _timeDifference;

    public DateSelectAlgorithm(TimeSpan timeDifference)
    {
        _timeDifference = timeDifference;
    }

    public IReadOnlyList<RestorePoint> SelectBlacklist(IReadOnlyList<RestorePoint> restorePoints)
    {
        var result = new List<RestorePoint>();

        if (restorePoints.Count == 0)
        {
            return result;
        }

        DateTime lastDate = restorePoints.Select(restorePoint => restorePoint.CreationTime).Max();
        result = restorePoints.Where(restorePoint => lastDate - restorePoint.CreationTime <= _timeDifference).ToList();

        return result;
    }
}
