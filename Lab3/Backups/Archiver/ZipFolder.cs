using System.IO.Compression;
using Backups.Exceptions;
using Backups.Repository;

namespace Backups.Archiver;

public class ZipFolder : IZipObject
{
    public ZipFolder(string name, IReadOnlyList<IZipObject> zipObjects)
    {
        ZipObjects = zipObjects;
        Name = name;
    }

    public string Name { get; }
    public IReadOnlyList<IZipObject> ZipObjects { get; }

    public IRepositoryObject GetCorrespondingRepositoryObject(ZipArchiveEntry entry)
    {
        return new Folder(() => GetAllItems(entry), Name.Remove(Name.Length - 1));
    }

    private IReadOnlyCollection<IRepositoryObject> GetAllItems(ZipArchiveEntry entry)
    {
        var items = new List<IRepositoryObject>();
        foreach (IZipObject item in ZipObjects)
        {
            items.Add(item.GetCorrespondingRepositoryObject(entry.Archive.GetEntry(item.Name) ?? throw ArchiverException.EntryAbsent(item.Name)));
        }

        return items;
    }
}
