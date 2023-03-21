using Backups.Repository;

namespace Backups.Extra.Logging;

public class RepositoryLogger : IRepository
{
    private readonly IRepository _target;
    private readonly ILogger _logger;

    public RepositoryLogger(IRepository target, ILogger logger)
    {
        _target = target;
        _logger = logger;
    }

    public void Delete(IRepositoryObject repositoryObject)
    {
        _logger.Log($"Delete {repositoryObject.Name}");
        _target.Delete(repositoryObject);
        _logger.Log($"Successfully deleted {repositoryObject.Name}");
    }

    public IRepositoryObject GetRepositoryObject(string localPath)
    {
        _logger.Log($"Create object {localPath}");
        IRepositoryObject result = _target.GetRepositoryObject(localPath);
        _logger.Log($"Successfully created object {result.Name}");
        return result;
    }

    public Stream OpenRead(string localPath)
    {
        _logger.Log($"Requested to open stream {localPath} through repository itself. Consider using IRepositoryObject.Open()");
        return _target.OpenRead(localPath);
    }

    public Stream OpenWrite(string localPath)
    {
        _logger.Log($"Open stream {localPath}");
        return _target.OpenWrite(localPath);
    }
}
