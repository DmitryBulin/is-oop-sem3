using Banks.Console.State;

namespace Banks.Console.Comand;

public class InitComand : IComandExecutor
{
    private readonly IState _mainProgramState;

    public InitComand(IState mainProgramState)
    {
        _mainProgramState = mainProgramState;
    }

    public string Execute(IStateMachine stateMachine)
    {
        if (stateMachine.Context.BankFactory is null)
        {
            return "No Bank Factory found, check if you built it";
        }

        stateMachine.Context.CentralBank = new CentralBank.CentralBank(stateMachine.Context.BankFactory, stateMachine.Context.TimeChannel);
        stateMachine.ChangeState(_mainProgramState);
        return "Initialized successfully!";
    }

    public string HelpInfo()
    {
        return "finish initial process and continue to main program";
    }
}
