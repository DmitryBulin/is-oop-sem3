using Banks.Account;
using Banks.Bank;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class BankTermsUpdateDebitComand : IComandExecutor<Guid, int, decimal>
{
    public string Execute(IStateMachine stateMachine, Guid argument1, int argument2, decimal argument3)
    {
        IBank? bank = stateMachine.Context.Banks.FirstOrDefault(bank => bank.Info.Id.Equals(argument1));
        if (bank is null)
        {
            return $"Failed to find bank with id {argument1}";
        }

        IAccountTerms terms = new DebitAccountTerms(argument3);
        bank.UpdateTerms(
            new List<AccountTermsUpdatePair>()
            {
                new AccountTermsUpdatePair(bank.AvailableTerms[argument2 - 1], terms),
            });
        return "Successfully added new term";
    }

    public string HelpInfo()
    {
        return "[BankId] [OriginalTermIndex] [InterestPercentage] - update terms for debit account type in bank";
    }
}
