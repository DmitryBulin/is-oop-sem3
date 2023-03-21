using Banks.Console.Comand;

namespace Banks.Console.State;

public class State : IState
{
    private readonly IComandHandler _comandChain;

    public State(IComandHandler comandChain)
    {
        _comandChain = comandChain;
    }

    public string HandleInput(string input, IStateMachine stateMachine)
    {
        return _comandChain.Handle(input, stateMachine);
    }
}
