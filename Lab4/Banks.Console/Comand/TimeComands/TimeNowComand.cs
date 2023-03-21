using Banks.Console.State;

namespace Banks.Console.Comand;

public class TimeNowComand : IComandExecutor
{
    public string Execute(IStateMachine stateMachine)
    {
        return $"Current time: {stateMachine.Context.TimeChannel.CurrentTime}";
    }

    public string HelpInfo()
    {
        return "show current day in system";
    }
}
