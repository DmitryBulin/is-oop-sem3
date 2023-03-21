using Banks.Console.State;

namespace Banks.Console.Comand;

public class ClientWithNameComand : IComandExecutor<string>
{
    public string Execute(IStateMachine stateMachine, string argument)
    {
        if (stateMachine.Context.ClientNameBuilder is null)
        {
            return "Name already set";
        }

        stateMachine.Context.ClientSecondNameBuilder = stateMachine.Context.ClientNameBuilder.WithName(argument);
        stateMachine.Context.ClientNameBuilder = null;
        return $"Set client name to {argument}";
    }

    public string HelpInfo()
    {
        return "[Name] - set name. REQUIRED FIRST";
    }
}
