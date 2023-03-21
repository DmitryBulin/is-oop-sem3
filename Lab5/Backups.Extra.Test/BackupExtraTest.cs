using Backups.Archiver;
using Backups.Backup;
using Backups.Extra.BackupExtra;
using Backups.Extra.BackupMapper;
using Backups.Extra.CleanAlgorithm;
using Backups.Extra.RollbackAlgorithm;
using Backups.Extra.SelectAlgorithm;
using Backups.StorageAlgorithm;
using Backups.Test;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Extra.Test;

public class BackupExtraTest
{
    [Theory]
    [InlineData(2, 3)]
    [InlineData(3, 3)]
    [InlineData(3, 2)]
    public void CreateSeveralPoints_ExtraPointsDeleted(int maxPoints, int createPoints)
    {
        var fileSystem = new MemoryFileSystem();
        var backupObjectMapper = new BackupObjectMapper();
        string backupObjectDirectory = UPath.DirectorySeparator + "temp";
        string backupCopyDirectory = UPath.DirectorySeparator + "backup";
        string fileObjectName = "text.txt";

        var fileObjectPath = UPath.Combine(backupObjectDirectory, fileObjectName);

        fileSystem.CreateDirectory(backupObjectDirectory);
        fileSystem.CreateDirectory(backupCopyDirectory);

        fileSystem.WriteAllText(fileObjectPath, "This is text file");

        var backupObjectRepository = new InMemoryRepository(backupObjectDirectory, fileSystem);
        var backupCopyRepository = new InMemoryRepository(backupCopyDirectory, fileSystem);
        var backup = new BackupExtra.BackupExtra(new TotalNumberAlgorithm(maxPoints), new DeleteAlgorithm());
        var backupTask = new BackupTaskExtra(
            backupCopyRepository,
            new SingleStorageAlgorithm(),
            new ZipArchiver(),
            backup,
            new SimpleRollback(),
            backupObjectMapper);

        IBackupObject fileObject = backupObjectMapper.CreateBackupObject(backupObjectRepository, fileObjectName);

        backupTask.AddObject(fileObject);

        for (int i = 0; i < createPoints; ++i)
        {
            backupTask.CreateRestorePoint();
        }

        Assert.True(backup.RestorePoints.Count <= maxPoints);
        Assert.True(fileSystem.EnumerateFileSystemEntries(backupCopyDirectory).Count() <= maxPoints);
    }
}
