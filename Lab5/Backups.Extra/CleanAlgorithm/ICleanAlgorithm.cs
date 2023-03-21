using Backups.Backup;

namespace Backups.Extra.CleanAlgorithm;

public interface ICleanAlgorithm
{
    IReadOnlyList<RestorePoint> Clean(IReadOnlyList<RestorePoint> allPoints, IReadOnlyList<RestorePoint> blackList);
}
