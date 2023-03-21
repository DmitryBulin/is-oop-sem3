using Banks.Account;
using Banks.Console.State;
using Banks.Transaction;

namespace Banks.Console.Comand;

public class TransactionNewComand : IComandExecutor<Guid, Guid, decimal>
{
    public string Execute(IStateMachine stateMachine, Guid argument1, Guid argument2, decimal argument3)
    {
        IAccount? sender = stateMachine.Context.Accounts.FirstOrDefault(account => account.Id.Equals(argument1));
        IAccount? catcher = stateMachine.Context.Accounts.FirstOrDefault(account => account.Id.Equals(argument2));
        if (sender is null)
        {
            return $"Failed to find sender {argument1}";
        }

        if (catcher is null)
        {
            return $"Failed to find catcher {argument2}";
        }

        ITransaction transaction = sender.Bank.TransitMoney(sender, catcher, new TransactionValue(argument3));
        stateMachine.Context.Transactions.Add(transaction);
        return $"Performed transaction {transaction.Id}";
    }

    public string HelpInfo()
    {
        return "[SenderId] [CatcherId] [amount] - send amount from Sender to Catcher";
    }
}
