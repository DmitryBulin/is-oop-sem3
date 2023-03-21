namespace Banks.Console.State;

public interface IState
{
    string HandleInput(string input, IStateMachine stateMachine);
}
