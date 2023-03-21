using Banks.Account;
using Banks.Client;
using Banks.Console.State;

namespace Banks.Console.Comand;

public class FactoryAddProxyVerifyComand : IComandExecutor<decimal, decimal>
{
    public string Execute(IStateMachine stateMachine, decimal argument1, decimal argument2)
    {
        stateMachine.Context.AccountCreatorBuilder
            .WithProxy(
                new AccountVerifyProxyCreator(
                    new ClientVerifier(), argument1, argument2));
        return $"Successfully added verify proxy with parameters {argument1} {argument2}";
    }

    public string HelpInfo()
    {
        return "[MaxAdd] [MaxSubtract] - proxy for limitating account transit cap";
    }
}
