using Backups.Backup;

namespace Backups.Extra.SelectAlgorithm;

public interface ISelectAlgorithm
{
    IReadOnlyList<RestorePoint> SelectBlacklist(IReadOnlyList<RestorePoint> restorePoints);
}
