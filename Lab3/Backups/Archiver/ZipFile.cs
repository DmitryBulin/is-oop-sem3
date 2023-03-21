using System.IO.Compression;
using Backups.Repository;

namespace Backups.Archiver;

public class ZipFile : IZipObject
{
    public ZipFile(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public IRepositoryObject GetCorrespondingRepositoryObject(ZipArchiveEntry entry)
    {
        return new Repository.File(() => OpenFile(entry), Name);
    }

    private static Stream OpenFile(ZipArchiveEntry entry)
    {
        return entry.Open();
    }
}
