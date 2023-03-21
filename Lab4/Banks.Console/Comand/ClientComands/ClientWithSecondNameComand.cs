using Banks.Console.State;

namespace Banks.Console.Comand;

public class ClientWithSecondNameComand : IComandExecutor<string>
{
    public string Execute(IStateMachine stateMachine, string argument)
    {
        if (stateMachine.Context.ClientSecondNameBuilder is null)
        {
            if (stateMachine.Context.ClientNameBuilder is null)
            {
                return "Second name already set";
            }

            return "Name required first";
        }

        stateMachine.Context.ClientInfoBuilder = stateMachine.Context.ClientSecondNameBuilder.WithSecondName(argument);
        stateMachine.Context.ClientSecondNameBuilder = null;
        return $"Set client second name to {argument}";
    }

    public string HelpInfo()
    {
        return "[SecondName] - set second name. REQUIRED SECOND";
    }
}
