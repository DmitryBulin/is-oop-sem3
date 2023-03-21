using Banks.Bank;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class BankDeleteComand : IComandExecutor<Guid, bool>
{
    public string Execute(IStateMachine stateMachine, Guid argument1, bool argument2)
    {
        if (stateMachine.Context.CentralBank is null)
        {
            return "Failed to find central bank";
        }

        IBank? bank = stateMachine.Context.Banks.FirstOrDefault(bank => bank.Info.Id.Equals(argument1));
        if (bank is null)
        {
            return $"Failed to find bank with id {argument1}";
        }

        if (bank.OpenAccounts.Count != 0)
        {
            if (!argument2)
            {
                return "Failed to delete bank: has open accounts";
            }

            bank.OpenAccounts.ToList().ForEach(account => bank.CloseAccount(account));
        }

        stateMachine.Context.CentralBank.DeleteBank(bank);
        return "Successfully deleted bank";
    }

    public string HelpInfo()
    {
        return "[BankId] (forced) - delete bank. NOTE: To delete Bank and it's accounts pass forced parameter";
    }
}
