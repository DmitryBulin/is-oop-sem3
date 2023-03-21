using System.IO.Compression;
using Backups.Repository;

namespace Backups.Archiver;

public interface IZipObject
{
    string Name { get; }
    IRepositoryObject GetCorrespondingRepositoryObject(ZipArchiveEntry entry);
}
