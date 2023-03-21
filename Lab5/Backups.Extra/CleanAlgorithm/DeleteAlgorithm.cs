using Backups.Backup;

namespace Backups.Extra.CleanAlgorithm;

public class DeleteAlgorithm : ICleanAlgorithm
{
    public IReadOnlyList<RestorePoint> Clean(IReadOnlyList<RestorePoint> allPoints, IReadOnlyList<RestorePoint> blackList)
    {
        foreach (RestorePoint point in blackList)
        {
            point.Storage.Clear();
        }

        return allPoints.Where(point => !blackList.Contains(point)).ToList();
    }
}
