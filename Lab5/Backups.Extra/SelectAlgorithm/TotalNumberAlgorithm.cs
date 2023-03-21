using Backups.Backup;

namespace Backups.Extra.SelectAlgorithm;

public class TotalNumberAlgorithm : ISelectAlgorithm
{
    private readonly int _maxNumber;

    public TotalNumberAlgorithm(int maxNumber)
    {
        if (maxNumber <= 0)
        {
            throw new Exception();
        }

        _maxNumber = maxNumber;
    }

    public IReadOnlyList<RestorePoint> SelectBlacklist(IReadOnlyList<RestorePoint> restorePoints)
    {
        if (restorePoints.Count <= _maxNumber)
        {
            return new List<RestorePoint>();
        }

        var result = new List<RestorePoint>();
        for (int i = 0; i < restorePoints.Count - _maxNumber; i++)
        {
            result.Add(restorePoints[i]);
        }

        return result;
    }
}
