using Backups.Archiver;
using Backups.Backup;
using Backups.StorageAlgorithm;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class MemoryFileSystemTest
{
    [Fact]
    public void CreateTwoRestorePoints_RestorePointsCreatedFilesSaved()
    {
        var fileSystem = new MemoryFileSystem();
        string backupObjectDirectory = UPath.DirectorySeparator + "temp";
        string backupCopyDirectory = UPath.DirectorySeparator + "backup";
        string fileObjectName = "text.txt";
        string folderObjectName = "folder";
        string imageObjectName = UPath.Combine(folderObjectName, "image.bmp").FullName;

        var fileObjectPath = UPath.Combine(backupObjectDirectory, fileObjectName);
        var folderObjectPath = UPath.Combine(backupObjectDirectory, folderObjectName);
        var imageObjectPath = UPath.Combine(backupObjectDirectory, imageObjectName);

        fileSystem.CreateDirectory(backupObjectDirectory);
        fileSystem.CreateDirectory(backupCopyDirectory);

        fileSystem.WriteAllText(fileObjectPath, "This is text file");
        fileSystem.CreateDirectory(folderObjectPath);
        fileSystem.WriteAllText(imageObjectPath, "This is image");

        var backupObjectRepository = new InMemoryRepository(backupObjectDirectory, fileSystem);
        var backupCopyRepository = new InMemoryRepository(backupCopyDirectory, fileSystem);
        var backup = new Backup.Backup();
        var backupTask = new BackupTask(backupCopyRepository, new SingleStorageAlgorithm(), new ZipArchiver(), backup);

        var fileObject = new BackupObject(backupObjectRepository, fileObjectName);
        var folderObject = new BackupObject(backupObjectRepository, folderObjectName);
        var imageObject = new BackupObject(backupObjectRepository, imageObjectName);

        backupTask.AddObject(fileObject);
        backupTask.AddObject(folderObject);
        backupTask.AddObject(imageObject);

        RestorePoint restorePoint = backupTask.CreateRestorePoint();

        var fileNames = restorePoint.Storage.GetRepositoryObjects().Select(repositoryObject => repositoryObject.Name).ToList();

        Assert.True(backup.RestorePoints.Count == 1);
        Assert.True(fileSystem.EnumerateFileSystemEntries(backupCopyDirectory).Count() == 1);
        Assert.Contains(fileObjectName, fileNames);
        Assert.Contains(folderObjectName, fileNames);
        Assert.Contains(imageObjectName, fileNames);
    }
}
