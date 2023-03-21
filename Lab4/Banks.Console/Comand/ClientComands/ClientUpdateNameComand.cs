using Banks.Client;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class ClientUpdateNameComand : IComandExecutor<Guid, string>
{
    public string Execute(IStateMachine stateMachine, Guid argument1, string argument2)
    {
        IClient? clientToUpdate = stateMachine.Context.Clients.FirstOrDefault(client => client.Info.Id.Equals(argument1));
        if (clientToUpdate is null)
        {
            return $"Failed to find client {argument1}";
        }

        clientToUpdate.Info.UpdateName(argument2);
        return $"Set client name to {argument2}";
    }

    public string HelpInfo()
    {
        return "[ClientId] [name] - update name of already exsiting client";
    }
}
