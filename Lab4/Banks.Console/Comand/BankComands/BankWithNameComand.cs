using Banks.Console.State;

namespace Banks.Console.Comand;

public class BankWithNameComand : IComandExecutor<string>
{
    public string Execute(IStateMachine stateMachine, string argument)
    {
        stateMachine.Context.BankName = argument;
        return $"Set bank name to {argument}";
    }

    public string HelpInfo()
    {
        return "[BankName] - set name";
    }
}
