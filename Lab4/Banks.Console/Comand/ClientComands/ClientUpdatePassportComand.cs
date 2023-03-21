using Banks.Client;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class ClientUpdatePassportComand : IComandExecutor<Guid, string>
{
    public string Execute(IStateMachine stateMachine, Guid argument1, string argument2)
    {
        IClient? clientToUpdate = stateMachine.Context.Clients.FirstOrDefault(client => client.Info.Id.Equals(argument1));
        if (clientToUpdate is null)
        {
            return $"Failed to find client {argument1}";
        }

        clientToUpdate.Info.UpdatePassportNumber(argument2);
        return $"Set client passport number to {argument2}";
    }

    public string HelpInfo()
    {
        return "[ClientId] [passport] - update passport of already exsiting client";
    }
}
