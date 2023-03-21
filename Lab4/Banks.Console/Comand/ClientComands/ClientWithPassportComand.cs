using Banks.Console.State;

namespace Banks.Console.Comand;

public class ClientWithPassportComand : IComandExecutor<string>
{
    public string Execute(IStateMachine stateMachine, string argument)
    {
        if (stateMachine.Context.ClientInfoBuilder is null)
        {
            return "Not completed required steps";
        }

        stateMachine.Context.ClientInfoBuilder.WithPassportNumber(argument);
        return $"Set client passport number to {argument}";
    }

    public string HelpInfo()
    {
        return "[PassportNumber] - set passport";
    }
}
