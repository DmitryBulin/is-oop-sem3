using Backups.Archiver;
using Backups.Backup;
using Backups.Repository;
using Backups.Storage;

namespace Backups.StorageAlgorithm;

public interface IStorageAlgorithm
{
    IStorage StoreData(IReadOnlyCollection<IBackupObject> backupObjects, IRepository repository, IArchiver archiver);
}
