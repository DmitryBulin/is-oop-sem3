namespace Banks.Console.Comand;

public static class ArgumentParser
{
    public static T? TryParse<T>(string input)
    {
        return (T)Convert.ChangeType(input, typeof(T));
    }
}