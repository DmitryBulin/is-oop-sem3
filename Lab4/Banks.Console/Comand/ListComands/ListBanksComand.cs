using Banks.Console.State;

namespace Banks.Console.Comand;

public class ListBanksComand : IComandExecutor
{
    public string Execute(IStateMachine stateMachine)
    {
        string response = "Existing banks: \n";
        stateMachine.Context.Banks.ForEach(bank => response += $"{bank.Info}\n");
        return response;
    }

    public string HelpInfo()
    {
        return "existing banks";
    }
}
