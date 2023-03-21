using Banks.Client;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class ClientBuildComand : IComandExecutor
{
    public string Execute(IStateMachine stateMachine)
    {
        if (stateMachine.Context.ClientInfoBuilder is null)
        {
            return "Failed to build client - required steps not completed";
        }

        IClientInfo clientInfo = stateMachine.Context.ClientInfoBuilder.Build();
        IClient client = new Client.Client(clientInfo);
        stateMachine.Context.Clients.Add(client);
        return $"Successfully created client {client.Info.Id}";
    }

    public string HelpInfo()
    {
        return "build new client with set data";
    }
}
