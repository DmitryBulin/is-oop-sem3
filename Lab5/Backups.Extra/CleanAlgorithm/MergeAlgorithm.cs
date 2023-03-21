using Backups.Backup;
using Backups.Extra.BackupExtra;
using Backups.Repository;
using Backups.Storage;

namespace Backups.Extra.CleanAlgorithm;

public class MergeAlgorithm : ICleanAlgorithm
{
    private readonly IBackupTaskExecutor _backupTask;

    public MergeAlgorithm(IBackupTaskExecutor backupTask)
    {
        _backupTask = backupTask;
    }

    public IReadOnlyList<RestorePoint> Clean(IReadOnlyList<RestorePoint> allPoints, IReadOnlyList<RestorePoint> blackList)
    {
        var result = new List<RestorePoint>();
        result.AddRange(allPoints.Where(point => !blackList.Contains(point)));

        RestorePoint mergedPoint = Merge(blackList);
        result.Add(mergedPoint);

        foreach (IStorage storage in blackList.Select(restorePoint => restorePoint.Storage))
        {
            storage.Clear();
        }

        return result;
    }

    private RestorePoint Merge(IReadOnlyList<RestorePoint> restorePoints)
    {
        if (restorePoints.Count == 0)
        {
            throw new Exception();
        }

        var totalRepositoryObjects = new List<IRepositoryObject>();
        DateTime creationTime = restorePoints.OrderBy(restorePoint => restorePoint.CreationTime).First().CreationTime;

        foreach (IRepositoryObject newRepositoryObject in restorePoints
            .OrderBy(restorePoint => restorePoint.CreationTime)
            .Reverse()
            .SelectMany(restorePoint => restorePoint.Storage.GetRepositoryObjects()))
        {
            IRepositoryObject? existingObject = totalRepositoryObjects.SingleOrDefault(repositoryObject => repositoryObject.Name == newRepositoryObject.Name);
            if (existingObject is not null)
            {
                totalRepositoryObjects.Remove(existingObject);
            }

            totalRepositoryObjects.Add(newRepositoryObject);
        }

        return _backupTask.CreateRestorePoint(
            creationTime,
            totalRepositoryObjects.Select(repoObject => new BackupObjectBridge(repoObject)).ToList());
    }
}
