using Banks.Console.State;

namespace Banks.Console.Comand;

public class FactoryInterestPeriodComand : IComandExecutor<int>
{
    public string Execute(IStateMachine stateMachine, int argument)
    {
        if (argument <= 0)
        {
            return "interest period must be positive";
        }

        stateMachine.Context.InterestPeriod = argument;
        return $"Successfully set interest period to {argument}";
    }

    public string HelpInfo()
    {
        return "[MaxPeriod] - number of days for maintaining accounts. Default is 30";
    }
}
