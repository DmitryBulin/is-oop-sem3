using Banks.Console.State;

namespace Banks.Console.Comand;

public class FactoryApplyComand : IComandExecutor
{
    public string Execute(IStateMachine stateMachine)
    {
        if (stateMachine.Context.CentralBank is null)
        {
            return "Central bank is unavailable. Try command 'init' ";
        }

        if (stateMachine.Context.BankFactory is null)
        {
            return "No bank factory found. Check if you built it";
        }

        stateMachine.Context.CentralBank.ChangeBankFactory(stateMachine.Context.BankFactory);
        return "Successfully applied bank factory to central bank";
    }

    public string HelpInfo()
    {
        return "sets current factory to use in the central bank";
    }
}
