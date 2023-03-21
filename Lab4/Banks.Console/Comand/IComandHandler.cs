using Banks.Console.State;

namespace Banks.Console.Comand;

public interface IComandHandler
{
    string Prefix { get; }
    string Handle(string input, IStateMachine stateMachine);
    string HelpInfo(string seed);
}
