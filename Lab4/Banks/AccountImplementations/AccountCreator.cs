using Banks.Bank;
using Banks.Client;

namespace Banks.Account;

public class AccountCreator : IAccountCreator
{
    private readonly IAccountProxyCreator _proxyCreator;

    private AccountCreator(IAccountProxyCreator proxyCreator)
    {
        _proxyCreator = proxyCreator;
    }

    public static AccountCreatorBuilder Builder => new AccountCreatorBuilder();

    public IAccount CreateAccount(
        IBank bank,
        IAccountTerms accountTerms,
        IClient accountOwner,
        IAccountProxyCreator additionalProxy,
        DateTime creationTime)
    {
        IAccount account = new Account(accountOwner, bank, accountTerms, DateOnly.FromDateTime(creationTime));
        account = account.AccountTerms.Wrap(account);
        account = _proxyCreator.Wrap(account);
        account = additionalProxy.Wrap(account);
        return account;
    }

    public class AccountCreatorBuilder
    {
        private readonly IAccountProxyCreator _topCreator = new EmptyProxyCreator();

        public AccountCreatorBuilder WithProxy(IAccountProxyCreator accountProxyCreator)
        {
            _topCreator.AddNext(accountProxyCreator);

            return this;
        }

        public AccountCreator Build()
        {
            return new AccountCreator(_topCreator);
        }
    }
}
