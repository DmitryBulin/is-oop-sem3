using Banks.Console.Context;

namespace Banks.Console.State;

public class StateMachine : IStateMachine
{
    private IState _currentState;

    public StateMachine(IContext context, IState initialState)
    {
        _currentState = initialState;
        Context = context;
    }

    public IContext Context { get; }

    public void ChangeState(IState state)
    {
        _currentState = state;
    }

    public string HandleInput(string input)
    {
        return _currentState.HandleInput(input, this);
    }
}
