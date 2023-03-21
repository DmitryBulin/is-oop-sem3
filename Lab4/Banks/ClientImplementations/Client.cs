using Banks.Account;
using Banks.Exceptions;

namespace Banks.Client;

public class Client : IClient
{
    private readonly List<IAccount> _accounts = new List<IAccount>();

    public Client(IClientInfo clientInfo)
    {
        Info = clientInfo;
    }

    public IClientInfo Info { get; }

    public IReadOnlyCollection<IAccount> Accounts => _accounts;

    public void AddAccount(IAccount account)
    {
        if (account.Owner != this)
        {
            throw ClientException.WrongOwner(Info.Id, account.Owner.Info.Id);
        }

        if (_accounts.Contains(account))
        {
            throw ClientException.AccountDuplication(Info.Id, account.Id);
        }

        _accounts.Add(account);
    }

    public void RemoveAccount(IAccount account)
    {
        if (account.Owner != this)
        {
            throw ClientException.WrongOwner(Info.Id, account.Owner.Info.Id);
        }

        if (!_accounts.Remove(account))
        {
            throw ClientException.AccountAbsence(Info.Id, account.Id);
        }
    }
}
