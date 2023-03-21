using Banks.Console.State;

namespace Banks.Console.Comand;

public class ListTransactionsComand : IComandExecutor
{
    public string Execute(IStateMachine stateMachine)
    {
        string response = "Transactions: \n";
        stateMachine.Context.Transactions.ForEach(transaction => response += $"{transaction.Id} \n");
        return response;
    }

    public string HelpInfo()
    {
        return "transactions";
    }
}
