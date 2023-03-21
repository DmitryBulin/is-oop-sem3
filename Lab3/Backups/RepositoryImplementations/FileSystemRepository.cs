using Backups.Exceptions;

namespace Backups.Repository;

public class FileSystemRepository : IRepository
{
    private readonly string _path;

    public FileSystemRepository(string path)
    {
        if (!Directory.Exists(path))
        {
            throw RepositoryException.RepositoryDirectoryAbsent(path);
        }

        _path = path;
    }

    public Stream OpenWrite(string localPath)
    {
        string filePath = Path.Combine(_path, localPath);
        Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? string.Empty);
        return System.IO.File.OpenWrite(filePath);
    }

    public Stream OpenRead(string localPath)
    {
        string filePath = Path.Combine(_path, localPath);
        return System.IO.File.OpenRead(filePath);
    }

    public IRepositoryObject GetRepositoryObject(string localPath)
    {
        string itemPath = Path.Combine(_path, localPath);
        if (Directory.Exists(itemPath))
        {
            return new Folder(() => GetAllItems(itemPath), localPath);
        }
        else if (System.IO.File.Exists(itemPath))
        {
            return new File(() => OpenFile(itemPath), localPath);
        }

        throw RepositoryException.RepositoryObjectCreation(localPath);
    }

    public void Delete(IRepositoryObject repositoryObject)
    {
        string itemPath = Path.Combine(_path, repositoryObject.Name);
        if (Directory.Exists(itemPath))
        {
            System.IO.Directory.Delete(itemPath);
        }
        else if (System.IO.File.Exists(itemPath))
        {
            System.IO.File.Delete(itemPath);
        }
        else
        {
            throw new Exception();
        }
    }

    public override string ToString() => $"Repository {_path}";

    private static Stream OpenFile(string path)
    {
        return System.IO.File.OpenRead(path);
    }

    private IReadOnlyCollection<IRepositoryObject> GetAllItems(string path)
    {
        var items = new List<IRepositoryObject>();
        foreach (string fullPath in Directory.GetFileSystemEntries(path))
        {
            items.Add(GetRepositoryObject(Path.GetRelativePath(_path, fullPath)));
        }

        return items;
    }
}
