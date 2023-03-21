using System.IO.Compression;
using Backups.Repository;
using Backups.Storage;
using Backups.Visitor;

namespace Backups.Archiver;

public class ZipArchiver : IArchiver
{
    public IStorage CreateArchive(IReadOnlyCollection<IRepositoryObject> repositoryObjects, IRepository repository, string archiveName)
    {
        string nameWithExtension = archiveName + ".zip";
        ZipVisitor visitor;
        Stream archiveStream = repository.OpenWrite(nameWithExtension);
        using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create))
        {
            visitor = new ZipVisitor(archive);
            foreach (IRepositoryObject repositoryObject in repositoryObjects)
            {
                repositoryObject.Accept(visitor);
            }
        }

        archiveStream.Close();

        return new ZipStorage(repository, nameWithExtension, new ZipFolder(archiveName, visitor.Links.Peek()));
    }
}
