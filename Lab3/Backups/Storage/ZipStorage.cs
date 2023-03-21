using System.IO.Compression;
using Backups.Archiver;
using Backups.Exceptions;
using Backups.Repository;

namespace Backups.Storage;

public class ZipStorage : IStorage
{
    private readonly IRepository _repository;
    private readonly string _localPath;
    private readonly ZipFolder _structureFolder;

    public ZipStorage(IRepository repository, string localPath, ZipFolder structureFolder)
    {
        _repository = repository;
        _localPath = localPath;
        _structureFolder = structureFolder;
    }

    public void Clear()
    {
        _repository.Delete(_repository.GetRepositoryObject(_localPath));
    }

    public IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects()
    {
        var archive = new ZipArchive(_repository.OpenRead(_localPath), ZipArchiveMode.Read);
        var items = new List<IRepositoryObject>();
        foreach (IZipObject item in _structureFolder.ZipObjects)
        {
            items.Add(item.GetCorrespondingRepositoryObject(archive.GetEntry(item.Name) ?? throw ArchiverException.EntryAbsent(item.Name)));
        }

        return items;
    }
}
