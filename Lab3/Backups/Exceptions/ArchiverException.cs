namespace Backups.Exceptions;

public class ArchiverException : BackupsException
{
    private ArchiverException(string message)
        : base(message)
    {
    }

    public static ArchiverException EntryAbsent(string entryName)
    {
        return new ArchiverException("Failed to get entry in archive with name " + entryName);
    }
}
