using Backups.Repository;
using Backups.Storage;

namespace Backups.Archiver;

public interface IArchiver
{
    IStorage CreateArchive(IReadOnlyCollection<IRepositoryObject> repositoryObjects, IRepository repository, string archiveName);
}