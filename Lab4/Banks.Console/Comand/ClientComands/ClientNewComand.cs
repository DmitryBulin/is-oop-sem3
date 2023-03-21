using Banks.Client;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class ClientNewComand : IComandExecutor
{
    public string Execute(IStateMachine stateMachine)
    {
        stateMachine.Context.ClientNameBuilder = ClientInfo.Builder;
        stateMachine.Context.ClientSecondNameBuilder = null;
        stateMachine.Context.ClientInfoBuilder = null;
        return "Ready for creating new client";
    }

    public string HelpInfo()
    {
        return "prepare to create new client";
    }
}
