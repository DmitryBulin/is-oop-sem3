using Backups.Exceptions;
using Backups.Repository;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class InMemoryRepository : IRepository
{
    private readonly string _path;
    private readonly MemoryFileSystem _fileSystem;

    public InMemoryRepository(UPath path, MemoryFileSystem fileSystem)
    {
        if (!fileSystem.DirectoryExists(path))
        {
            throw RepositoryException.RepositoryDirectoryAbsent(path.FullName);
        }

        _path = path.FullName;
        _fileSystem = fileSystem;
    }

    public Stream OpenRead(string localPath)
    {
        var filePath = UPath.Combine(_path, localPath);
        _fileSystem.CreateDirectory(filePath.GetDirectory().FullName);
        return _fileSystem.OpenFile(filePath, FileMode.Open, FileAccess.Read);
    }

    public Stream OpenWrite(string localPath)
    {
        var filePath = UPath.Combine(_path, localPath);
        return _fileSystem.OpenFile(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
    }

    public IRepositoryObject GetRepositoryObject(string localPath)
    {
        string itemPath = _path + '/' + localPath;
        if (_fileSystem.DirectoryExists(itemPath))
        {
            return new Folder(() => GetAllItems(itemPath), localPath);
        }
        else if (_fileSystem.FileExists(itemPath))
        {
            return new Repository.File(() => OpenFile(itemPath), localPath);
        }

        throw RepositoryException.RepositoryObjectCreation(itemPath);
    }

    public void Delete(IRepositoryObject repositoryObject)
    {
        string itemPath = _path + '/' + repositoryObject.Name;

        if (_fileSystem.FileExists(itemPath))
        {
            _fileSystem.DeleteFile(itemPath);
        }
        else if (_fileSystem.DirectoryExists(itemPath))
        {
            _fileSystem.DeleteDirectory(itemPath, true);
        }
        else
        {
            throw new Exception();
        }
    }

    private Stream OpenFile(string path)
    {
        return _fileSystem.OpenFile(path, FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    private IReadOnlyCollection<IRepositoryObject> GetAllItems(string path)
    {
        var items = new List<IRepositoryObject>();
        foreach (string fullPath in _fileSystem.EnumerateFileSystemEntries(path).Select(entry => entry.FullName))
        {
            items.Add(GetRepositoryObject(fullPath.Replace(_path, string.Empty)));
        }

        return items;
    }
}
