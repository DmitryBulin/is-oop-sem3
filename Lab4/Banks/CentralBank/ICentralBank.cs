using Banks.Account;
using Banks.Bank;
using Banks.Transaction;

namespace Banks.CentralBank;

public interface ICentralBank
{
    IReadOnlyCollection<IBank> Banks { get; }
    void ChangeBankFactory(IBankFactory bankFactory);
    IBank RegisterNewBank(BankInfo bankInfo, IAccountProxyCreator additionalProxy);
    void DeleteBank(IBank bank);
    ITransaction TransitMoneyBetweenBanks(IAccount sender, IAccount catcher, TransactionValue amount);
}
