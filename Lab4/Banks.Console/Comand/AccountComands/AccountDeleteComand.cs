using Banks.Account;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class AccountDeleteComand : IComandExecutor<Guid>
{
    public string Execute(IStateMachine stateMachine, Guid argument)
    {
        IAccount? account = stateMachine.Context.Accounts.FirstOrDefault(account => account.Id.Equals(argument));
        if (account is null)
        {
            return $"Failed to find account with id {argument}";
        }

        stateMachine.Context.Accounts.Remove(account);
        account.Bank.CloseAccount(account);
        return "Successfully removed account";
    }

    public string HelpInfo()
    {
        return "[AccountId] - delete account";
    }
}
