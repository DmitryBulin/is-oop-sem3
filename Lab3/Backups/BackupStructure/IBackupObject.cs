using Backups.Repository;

namespace Backups.Backup;

public interface IBackupObject
{
    IRepositoryObject GetCorrespondingRepositoryObject();
}
