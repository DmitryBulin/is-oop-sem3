namespace Backups.Exceptions;

public abstract class BackupsException : Exception
{
    public BackupsException(string message)
        : base(message)
    {
    }
}
