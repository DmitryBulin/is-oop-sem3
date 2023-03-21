using Backups.Archiver;
using Backups.Backup;
using Backups.Extra.BackupMapper;
using Backups.Extra.RollbackAlgorithm;
using Backups.Repository;
using Backups.StorageAlgorithm;

namespace Backups.Extra.BackupExtra;

public class BackupTaskExtra : BackupTask, IBackupTaskExtra
{
    private readonly IRollbackAlgorithm _rollbackAlgorithm;
    private readonly IBackupObjectMapper _backupObjectMapper;

    public BackupTaskExtra(
        IRepository repository,
        IStorageAlgorithm algorithm,
        IArchiver archiver,
        IBackup backup,
        IRollbackAlgorithm rollbackAlgorithm,
        IBackupObjectMapper backupObjectMapper)
        : base(repository, algorithm, archiver, backup)
    {
        _rollbackAlgorithm = rollbackAlgorithm;
        _backupObjectMapper = backupObjectMapper;
    }

    public void RollBack(RestorePoint restorePoint)
    {
        foreach (IRepositoryObject repositoryObject in restorePoint.Storage.GetRepositoryObjects())
        {
            IBackupObject? backupObject = restorePoint.BackupObjects
                .SingleOrDefault(backupObject => backupObject.GetCorrespondingRepositoryObject().Name == repositoryObject.Name);
            if (backupObject is null)
            {
                throw new Exception();
            }

            _rollbackAlgorithm.Restore(repositoryObject, _backupObjectMapper.GetRepository(backupObject));
        }
    }

    public void RollBack(RestorePoint restorePoint, IRepository differentLocation)
    {
        foreach (IRepositoryObject repositoryObject in restorePoint.Storage.GetRepositoryObjects())
        {
            _rollbackAlgorithm.Restore(repositoryObject, differentLocation);
        }
    }
}
