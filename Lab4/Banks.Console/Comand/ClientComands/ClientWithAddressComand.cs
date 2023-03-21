using Banks.Console.State;

namespace Banks.Console.Comand;

public class ClientWithAddressComand : IComandExecutor<string>
{
    public string Execute(IStateMachine stateMachine, string argument)
    {
        if (stateMachine.Context.ClientInfoBuilder is null)
        {
            return "Not completed required steps";
        }

        stateMachine.Context.ClientInfoBuilder.WithAddress(argument);
        return $"Set client address to {argument}";
    }

    public string HelpInfo()
    {
        return "[Address] - set address";
    }
}
