using Backups.Backup;
using Backups.Repository;

namespace Backups.Extra.BackupMapper;

public interface IBackupObjectMapper
{
    IRepository GetRepository(IBackupObject backupObject);
}
