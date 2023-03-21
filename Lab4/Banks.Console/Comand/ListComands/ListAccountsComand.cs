using Banks.Console.State;

namespace Banks.Console.Comand;

public class ListAccountsComand : IComandExecutor
{
    public string Execute(IStateMachine stateMachine)
    {
        string response = "Existing accounts: \n";
        stateMachine.Context.Accounts.ForEach(account => response += $"{account} \n");
        return response;
    }

    public string HelpInfo()
    {
        return "existing accounts";
    }
}
