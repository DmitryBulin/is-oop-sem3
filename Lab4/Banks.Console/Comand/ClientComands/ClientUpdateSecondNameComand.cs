using Banks.Client;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class ClientUpdateSecondNameComand : IComandExecutor<Guid, string>
{
    public string Execute(IStateMachine stateMachine, Guid argument1, string argument2)
    {
        IClient? clientToUpdate = stateMachine.Context.Clients.FirstOrDefault(client => client.Info.Id.Equals(argument1));
        if (clientToUpdate is null)
        {
            return $"Failed to find client {argument1}";
        }

        clientToUpdate.Info.UpdateSecondName(argument2);
        return $"Set client second name to {argument2}";
    }

    public string HelpInfo()
    {
        return "[ClientId] [second name] - update second name of already exsiting client";
    }
}
