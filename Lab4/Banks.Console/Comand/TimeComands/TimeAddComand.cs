using Banks.Console.State;

namespace Banks.Console.Comand;

public class TimeAddComand : IComandExecutor<int>
{
    public string Execute(IStateMachine stateMachine, int argument)
    {
        if (argument <= 0)
        {
            return "Day count must be positive";
        }

        DateTime newDate = stateMachine.Context.TimeChannel.CurrentTime.AddDays(argument);
        stateMachine.Context.TimeChannel.Notify(newDate);
        return $"Successfully added {argument} days";
    }

    public string HelpInfo()
    {
        return "[DayCount] - notify system that today is {time now} + DayCount days";
    }
}
