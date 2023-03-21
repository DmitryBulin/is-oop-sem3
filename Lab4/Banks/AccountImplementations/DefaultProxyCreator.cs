using Banks.Exceptions;

namespace Banks.Account;

public class DefaultProxyCreator : IAccountProxyCreator
{
    public void AddNext(IAccountProxyCreator creator)
    {
        throw ProxyCreatorException.DefaultProxyInMiddleOfChain();
    }

    public IAccount Wrap(IAccount account)
    {
        return account;
    }
}