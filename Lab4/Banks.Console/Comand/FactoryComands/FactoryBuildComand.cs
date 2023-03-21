using Banks.Bank;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class FactoryBuildComand : IComandExecutor
{
    public string Execute(IStateMachine stateMachine)
    {
        stateMachine.Context.BankFactory = new BankFactory(
            stateMachine.Context.AccountCreatorBuilder.Build(),
            stateMachine.Context.InterestPeriod);
        return "Successfully built new bank factory!";
    }

    public string HelpInfo()
    {
        return "finalizes the factory and creates new factory. NOTE: Doesn't automatically change it in central bank";
    }
}
