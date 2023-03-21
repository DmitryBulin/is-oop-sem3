namespace Banks.Account;

public class EmptyProxyCreator : IAccountProxyCreator
{
    private IAccountProxyCreator _next = new DefaultProxyCreator();

    public void AddNext(IAccountProxyCreator creator)
    {
        if (_next is DefaultProxyCreator)
        {
            _next = creator;
        }
        else
        {
            _next.AddNext(creator);
        }
    }

    public IAccount Wrap(IAccount account)
    {
        return _next.Wrap(account);
    }
}
