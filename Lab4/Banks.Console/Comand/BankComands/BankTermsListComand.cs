using Banks.Account;
using Banks.Bank;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class BankTermsListComand : IComandExecutor<Guid>
{
    public string Execute(IStateMachine stateMachine, Guid argument)
    {
        IBank? bank = stateMachine.Context.Banks.FirstOrDefault(bank => bank.Info.Id.Equals(argument));
        if (bank is null)
        {
            return $"Failed to find bank with id {argument}";
        }

        string response = "Available terms: \n";
        int index = 1;
        foreach (IAccountTerms accountTerms in bank.AvailableTerms)
        {
            response += $"{index}. {accountTerms}\n";
            index++;
        }

        return response;
    }

    public string HelpInfo()
    {
        return "get list of current terms";
    }
}
