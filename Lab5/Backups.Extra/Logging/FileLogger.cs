namespace Backups.Extra.Logging;

public class FileLogger : ILogger
{
    private readonly string _filePath;

    public FileLogger(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new Exception();
        }

        _filePath = filePath;
    }

    public void Log(string message)
    {
        File.AppendAllLines(_filePath, new List<string>() { message });
    }
}
