using Banks.Console.State;

namespace Banks.Console.Comand;

public class ListClientsComand : IComandExecutor
{
    public string Execute(IStateMachine stateMachine)
    {
        string response = "Existing clients: \n";
        stateMachine.Context.Clients.ForEach(client => response += $"{client.Info} \n");
        return response;
    }

    public string HelpInfo()
    {
        return "existing clients";
    }
}
