using Banks.Console.State;

namespace Banks.Console.Comand;

public class ArgumentProxy : IComandHandler
{
    private readonly IComandExecutor _comand;

    public ArgumentProxy(string prefix, IComandExecutor comand)
    {
        _comand = comand;
        Prefix = prefix;
    }

    public string Prefix { get; }

    public string HelpInfo(string seed) => $"{seed}{Prefix} - {_comand.HelpInfo()} \n";

    public string Handle(string input, IStateMachine stateMachine) => _comand.Execute(stateMachine);
}

public class ArgumentProxy<T1> : IComandHandler
{
    private readonly IComandExecutor<T1> _comand;

    public ArgumentProxy(string prefix, IComandExecutor<T1> comand)
    {
        _comand = comand;
        Prefix = prefix;
    }

    public string Prefix { get; }

    public string HelpInfo(string seed) => $"{seed}{Prefix} - {_comand.HelpInfo()} \n";

    public string Handle(string input, IStateMachine stateMachine)
    {
        T1? argument = ArgumentParser.TryParse<T1>(input);
        if (argument is null)
        {
            return "Failed to parse arguments";
        }

        return _comand.Execute(stateMachine, argument);
    }
}

public class ArgumentProxy<T1, T2> : IComandHandler
{
    private readonly IComandExecutor<T1, T2> _comand;

    public ArgumentProxy(string prefix, IComandExecutor<T1, T2> comand)
    {
        _comand = comand;
        Prefix = prefix;
    }

    public string Prefix { get; }

    public string HelpInfo(string seed) => $"{seed}{Prefix} - {_comand.HelpInfo()}\n";

    public string Handle(string input, IStateMachine stateMachine)
    {
        string[] subStrings = input.Split(" ");

        if (subStrings.Length != 2)
        {
            return "Expected 2 arguments";
        }

        T1? argument1 = ArgumentParser.TryParse<T1>(subStrings[0]);
        T2? argument2 = ArgumentParser.TryParse<T2>(subStrings[1]);

        if (argument1 is null || argument2 is null)
        {
            return "Failed to parse arguments";
        }

        return _comand.Execute(stateMachine, argument1, argument2);
    }
}

public class ArgumentProxy<T1, T2, T3> : IComandHandler
{
    private readonly IComandExecutor<T1, T2, T3> _comand;

    public ArgumentProxy(string prefix, IComandExecutor<T1, T2, T3> comand)
    {
        _comand = comand;
        Prefix = prefix;
    }

    public string Prefix { get; }

    public string HelpInfo(string seed) => $"{seed}{Prefix} - {_comand.HelpInfo()}\n";

    public string Handle(string input, IStateMachine stateMachine)
    {
        string[] subStrings = input.Split(" ");

        if (subStrings.Length != 3)
        {
            return "Expected 3 arguments";
        }

        T1? argument1 = ArgumentParser.TryParse<T1>(subStrings[0]);
        T2? argument2 = ArgumentParser.TryParse<T2>(subStrings[1]);
        T3? argument3 = ArgumentParser.TryParse<T3>(subStrings[2]);

        if (argument1 is null || argument2 is null || argument3 is null)
        {
            return "Failed to parse arguments";
        }

        return _comand.Execute(stateMachine, argument1, argument2, argument3);
    }
}

public class ArgumentProxy<T1, T2, T3, T4> : IComandHandler
{
    private readonly IComandExecutor<T1, T2, T3, T4> _comand;

    public ArgumentProxy(string prefix, IComandExecutor<T1, T2, T3, T4> comand)
    {
        _comand = comand;
        Prefix = prefix;
    }

    public string Prefix { get; }

    public string HelpInfo(string seed) => $"{seed}{Prefix} - {_comand.HelpInfo()} \n";

    public string Handle(string input, IStateMachine stateMachine)
    {
        string[] subStrings = input.Split(" ");

        if (subStrings.Length != 4)
        {
            return "Expected 4 arguments";
        }

        T1? argument1 = ArgumentParser.TryParse<T1>(subStrings[0]);
        T2? argument2 = ArgumentParser.TryParse<T2>(subStrings[1]);
        T3? argument3 = ArgumentParser.TryParse<T3>(subStrings[2]);
        T4? argument4 = ArgumentParser.TryParse<T4>(subStrings[3]);

        if (argument1 is null || argument2 is null || argument3 is null || argument4 is null)
        {
            return "Failed to parse arguments";
        }

        return _comand.Execute(stateMachine, argument1, argument2, argument3, argument4);
    }
}