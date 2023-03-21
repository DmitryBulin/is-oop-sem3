namespace Backups.Exceptions;

public class RepositoryException : BackupsException
{
    private RepositoryException(string message)
        : base(message)
    {
    }

    public static RepositoryException RepositoryObjectCreation(string localPath)
    {
        return new RepositoryException($"Failed to create repository object from path {localPath}");
    }

    public static RepositoryException RepositoryDirectoryAbsent(string repositoryPath)
    {
        return new RepositoryException($"Failed to initialise repository from path {repositoryPath}");
    }
}
