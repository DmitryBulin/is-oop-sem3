using Banks.Bank;
using Banks.Client;

namespace Banks.Account;

public interface IAccountCreator
{
    IAccount CreateAccount(
        IBank bank,
        IAccountTerms accountTerms,
        IClient accountOwner,
        IAccountProxyCreator additionalProxy,
        DateTime creationTime);
}