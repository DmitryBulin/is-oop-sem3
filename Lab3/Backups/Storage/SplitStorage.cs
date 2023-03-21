using Backups.Repository;

namespace Backups.Storage;

public class SplitStorage : IStorage
{
    private readonly IReadOnlyCollection<IStorage> _storages;

    public SplitStorage(IReadOnlyCollection<IStorage> storages)
    {
        _storages = storages;
    }

    public void Clear()
    {
        foreach (IStorage storage in _storages)
        {
            storage.Clear();
        }
    }

    public IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects()
    {
        return _storages.SelectMany(storage => storage.GetRepositoryObjects()).ToList();
    }
}
