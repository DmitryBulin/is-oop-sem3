using Banks.Console.State;

namespace Banks.Console.Comand;

public class ExitComand : IComandExecutor
{
    public string Execute(IStateMachine stateMachine)
    {
        Environment.Exit(0);
        return "Closing application..";
    }

    public string HelpInfo()
    {
        return "close program";
    }
}
