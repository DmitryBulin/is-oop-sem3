namespace Backups.Extra.Logging;

public class LoggerPrefixDecorator : ILogger
{
    private readonly ILogger _target;
    private readonly Func<string> _prefixFunc;
    public LoggerPrefixDecorator(ILogger target, Func<string> prefixFunc)
    {
        _target = target;
        _prefixFunc = prefixFunc;
    }

    public LoggerPrefixDecorator(ILogger target, string prefix)
        : this(target, () => prefix)
    {
        if (string.IsNullOrEmpty(prefix))
        {
            throw new Exception();
        }
    }

    public void Log(string message)
    {
        _target.Log($"{_prefixFunc.Invoke()}{message}");
    }
}
