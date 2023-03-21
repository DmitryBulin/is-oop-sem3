using Banks.Account;
using Banks.Bank;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class BankTermsAddCreditComand : IComandExecutor<Guid, decimal, decimal>
{
    public string Execute(IStateMachine stateMachine, Guid argument1, decimal argument2, decimal argument3)
    {
        IBank? bank = stateMachine.Context.Banks.FirstOrDefault(bank => bank.Info.Id.Equals(argument1));
        if (bank is null)
        {
            return $"Failed to find bank with id {argument1}";
        }

        IAccountTerms terms = new CreditAccountTerms(argument2, argument3);
        bank.AddNewTerms(new List<IAccountTerms>() { terms });
        return "Successfully added new term";
    }

    public string HelpInfo()
    {
        return "[CreditLimit] [OverdraftCommission] - credit account type";
    }
}
