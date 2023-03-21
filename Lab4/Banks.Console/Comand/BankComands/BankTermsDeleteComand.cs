using Banks.Account;
using Banks.Bank;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class BankTermsDeleteComand : IComandExecutor<Guid, int>
{
    public string Execute(IStateMachine stateMachine, Guid argument1, int argument2)
    {
        IBank? bank = stateMachine.Context.Banks.FirstOrDefault(bank => bank.Info.Id.Equals(argument1));
        if (bank is null)
        {
            return $"Failed to find bank with id {argument1}";
        }

        if (argument2 <= 0 || argument2 > bank.AvailableTerms.Count)
        {
            return "Term index is out of range";
        }

        bank.DeleteTerms(new List<IAccountTerms>() { bank.AvailableTerms[argument2 - 1] });
        return $"Successfully delete term with index {argument2}";
    }

    public string HelpInfo()
    {
        return "[BankId] [TermsIndex] - delete terms in bank by index";
    }
}
