using Banks.Account;

namespace Banks.Client;

public interface IClient
{
    IClientInfo Info { get; }
    IReadOnlyCollection<IAccount> Accounts { get; }
    void AddAccount(IAccount account);
    void RemoveAccount(IAccount account);
}