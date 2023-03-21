using Banks.Console.State;

namespace Banks.Console.Comand;

public interface IComandExecutor
{
    string Execute(IStateMachine stateMachine);
    string HelpInfo();
}

public interface IComandExecutor<T1>
{
    string Execute(IStateMachine stateMachine, T1 argument);
    string HelpInfo();
}

public interface IComandExecutor<T1, T2>
{
    string Execute(IStateMachine stateMachine, T1 argument1, T2 argument2);
    string HelpInfo();
}

public interface IComandExecutor<T1, T2, T3>
{
    string Execute(IStateMachine stateMachine, T1 argument1, T2 argument2, T3 argument3);
    string HelpInfo();
}

public interface IComandExecutor<T1, T2, T3, T4>
{
    string Execute(IStateMachine stateMachine, T1 argument1, T2 argument2, T3 argument3, T4 argument4);
    string HelpInfo();
}