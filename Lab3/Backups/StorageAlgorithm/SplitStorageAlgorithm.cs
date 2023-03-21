using Backups.Archiver;
using Backups.Backup;
using Backups.Repository;
using Backups.Storage;

namespace Backups.StorageAlgorithm;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    public IStorage StoreData(IReadOnlyCollection<IBackupObject> backupObjects, IRepository repository, IArchiver archiver)
    {
        var storages = new List<IStorage>();
        foreach (IBackupObject backupObject in backupObjects)
        {
            IRepositoryObject repositoryObject = backupObject.GetCorrespondingRepositoryObject();
            string archiveName = Path.Combine(DateTime.UtcNow.ToString("MM-dd-yy-H-mm-ss"), Path.GetFileName(repositoryObject.Name));
            IStorage storage = archiver.CreateArchive(new List<IRepositoryObject>() { repositoryObject }, repository, archiveName);
            storages.Add(storage);
        }

        return new SplitStorage(storages);
    }
}
