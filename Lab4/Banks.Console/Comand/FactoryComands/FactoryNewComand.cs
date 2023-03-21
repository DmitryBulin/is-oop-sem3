using Banks.Account;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class FactoryNewComand : IComandExecutor
{
    public string Execute(IStateMachine stateMachine)
    {
        stateMachine.Context.AccountCreatorBuilder = AccountCreator.Builder;
        stateMachine.Context.AccountCreator = null;
        stateMachine.Context.BankFactory = null;
        stateMachine.Context.InterestPeriod = 30;
        return "Ready to create new factory";
    }

    public string HelpInfo() => "prepare to create new factory";
}
