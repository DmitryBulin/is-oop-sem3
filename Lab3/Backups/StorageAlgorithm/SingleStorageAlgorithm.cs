using Backups.Archiver;
using Backups.Backup;
using Backups.Repository;
using Backups.Storage;

namespace Backups.StorageAlgorithm;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    public IStorage StoreData(IReadOnlyCollection<IBackupObject> backupObjects, IRepository repository, IArchiver archiver)
    {
        IReadOnlyCollection<IRepositoryObject> repositoryObjects = backupObjects
            .Select(backupObject => backupObject.GetCorrespondingRepositoryObject())
            .ToList();
        return archiver.CreateArchive(repositoryObjects, repository, DateTime.UtcNow.ToString("MM-dd-yy-H-mm-ss"));
    }
}
