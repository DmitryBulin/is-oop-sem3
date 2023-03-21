namespace Banks.Account;

public interface IAccountProxyCreator
{
    void AddNext(IAccountProxyCreator creator);
    IAccount Wrap(IAccount account);
}