using Banks.Console.Context;

namespace Banks.Console.State;

public interface IStateMachine
{
    IContext Context { get; }
    void ChangeState(IState state);
    string HandleInput(string input);
}
