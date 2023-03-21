using Banks.Account;
using Banks.Bank;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class BankTermsUpdateCreditComand : IComandExecutor<Guid, int, decimal, decimal>
{
    public string Execute(IStateMachine stateMachine, Guid argument1, int argument2, decimal argument3, decimal argument4)
    {
        IBank? bank = stateMachine.Context.Banks.FirstOrDefault(bank => bank.Info.Id.Equals(argument1));
        if (bank is null)
        {
            return $"Failed to find bank with id {argument1}";
        }

        IAccountTerms terms = new CreditAccountTerms(argument3, argument4);
        bank.UpdateTerms(
            new List<AccountTermsUpdatePair>()
            {
                new AccountTermsUpdatePair(bank.AvailableTerms[argument2 - 1], terms),
            });
        return "Successfully added new term";
    }

    public string HelpInfo()
    {
        return "[BankId] [OriginalTermIndex] [CreditLimit] [OverdraftCommission] - update terms for credit account type in bank";
    }
}
