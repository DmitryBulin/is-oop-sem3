using Banks.Account;
using Banks.Bank;
using Banks.Client;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class AccountNewComand : IComandExecutor<Guid, Guid, int>
{
    public string Execute(IStateMachine stateMachine, Guid argument1, Guid argument2, int argument3)
    {
        IBank? bank = stateMachine.Context.Banks.FirstOrDefault(bank => bank.Info.Id.Equals(argument1));
        IClient? client = stateMachine.Context.Clients.FirstOrDefault(client => client.Info.Id.Equals(argument2));

        if (bank is null)
        {
            return $"Failed to find bank {argument1}";
        }

        if (client is null)
        {
            return $"Failed to find client {argument2}";
        }

        if (argument3 <= 0 || argument3 > bank.AvailableTerms.Count)
        {
            return "Terms index is out of range";
        }

        IAccount account = bank.OpenAccount(client, bank.AvailableTerms[argument3 - 1]);
        stateMachine.Context.Accounts.Add(account);
        return "Successfully created account " + account.Id;
    }

    public string HelpInfo()
    {
        return "[BankId] [ClientId] [AccountTermsIndex] - create new account with Terms for Client in Bank";
    }
}
