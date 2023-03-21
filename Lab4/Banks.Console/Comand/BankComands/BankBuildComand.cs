using Banks.Bank;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class BankBuildComand : IComandExecutor
{
    public string Execute(IStateMachine stateMachine)
    {
        if (stateMachine.Context.CentralBank is null)
        {
            return "Failed to find central bank";
        }

        var bankInfo = new BankInfo(stateMachine.Context.BankName);
        IBank bank = stateMachine.Context.CentralBank.RegisterNewBank(bankInfo, stateMachine.Context.TopProxy);
        stateMachine.Context.Banks.Add(bank);
        return $"Successfully built bank {bank.Info.Id}";
    }

    public string HelpInfo()
    {
        return "create new bank";
    }
}
