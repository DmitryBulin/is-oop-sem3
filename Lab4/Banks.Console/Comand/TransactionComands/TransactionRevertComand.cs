using Banks.Console.State;
using Banks.Transaction;

namespace Banks.Console.Comand;

public class TransactionRevertComand : IComandExecutor<Guid>
{
    public string Execute(IStateMachine stateMachine, Guid argument)
    {
        ITransaction? transactionToRevert = stateMachine.Context.Transactions.FirstOrDefault(transaction => transaction.Id.Equals(argument));
        if (transactionToRevert is null)
        {
            return $"Failed to find transaction {argument}";
        }

        transactionToRevert.Revert();
        return $"Successfully reverted transaction {argument}";
    }

    public string HelpInfo()
    {
        return "[TransactionId] - revert Transaction";
    }
}
