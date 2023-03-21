using Banks.Client;

namespace Banks.Account;

public class AccountVerifyProxyCreator : IAccountProxyCreator
{
    private readonly IClientVerifier _clientVerifier;
    private readonly decimal _maxAddAmount;
    private readonly decimal _maxSubstractAmount;
    private IAccountProxyCreator _next = new DefaultProxyCreator();

    public AccountVerifyProxyCreator(IClientVerifier clientVerifier, decimal maxAddAmount, decimal maxSubstractAmount)
    {
        _clientVerifier = clientVerifier;
        _maxAddAmount = maxAddAmount;
        _maxSubstractAmount = maxSubstractAmount;
    }

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
        IAccount verifyProxy = new AccountVerifyProxy(_clientVerifier, account, _maxAddAmount, _maxSubstractAmount);
        return _next.Wrap(verifyProxy);
    }
}
