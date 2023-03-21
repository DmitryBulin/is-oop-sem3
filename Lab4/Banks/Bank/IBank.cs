using Banks.Account;
using Banks.Client;
using Banks.Transaction;
using Banks.UpdateChannel;

namespace Banks.Bank;

public interface IBank
{
    BankInfo Info { get; }
    IReadOnlyList<IAccountTerms> AvailableTerms { get; }
    IReadOnlyList<IAccount> OpenAccounts { get; }
    ITermUpdateChannel TermUpdate { get; }
    IAccount OpenAccount(IClient client, IAccountTerms terms);
    void CloseAccount(IAccount account);
    void UpdateTerms(IReadOnlyList<AccountTermsUpdatePair> terms);
    void AddNewTerms(IReadOnlyList<IAccountTerms> terms);
    void DeleteTerms(IReadOnlyList<IAccountTerms> terms);
    ITransaction TransitMoney(IAccount sender, IAccount catcher, TransactionValue amount);
}
