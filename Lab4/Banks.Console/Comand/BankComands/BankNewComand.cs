using Banks.Account;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class BankNewComand : IComandExecutor
{
    public string Execute(IStateMachine stateMachine)
    {
        stateMachine.Context.TopProxy = new EmptyProxyCreator();
        stateMachine.Context.BankName = string.Empty;
        return "Ready to create new bank";
    }

    public string HelpInfo()
    {
        return "prepare to create new bank";
    }
}
