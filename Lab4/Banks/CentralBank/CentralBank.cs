using Banks.Account;
using Banks.Bank;
using Banks.Exceptions;
using Banks.Transaction;
using Banks.UpdateChannel;

namespace Banks.CentralBank;

public class CentralBank : ICentralBank
{
    private readonly List<IBank> _banks = new List<IBank>();
    private readonly ITimeUpdateChannel _timeBroadcaster;
    private IBankFactory _bankFactory;

    public CentralBank(IBankFactory bankFactory, ITimeUpdateChannel timeBroadcaster)
    {
        _bankFactory = bankFactory;
        _timeBroadcaster = timeBroadcaster;
    }

    public IReadOnlyCollection<IBank> Banks => _banks;

    public void ChangeBankFactory(IBankFactory bankFactory)
    {
        _bankFactory = bankFactory;
    }

    public void DeleteBank(IBank bank)
    {
        if (bank.OpenAccounts.Count != 0)
        {
            throw CentralBankException.BankDeleteAccountsExist(bank.Info.Id);
        }

        if (!_banks.Remove(bank))
        {
            throw CentralBankException.BankAbsence(bank.Info.Id);
        }
    }

    public IBank RegisterNewBank(BankInfo bankInfo, IAccountProxyCreator additionalProxy)
    {
        if (_banks.Any(bank => bank.Info == bankInfo))
        {
            throw CentralBankException.BankDuplication(bankInfo.Id);
        }

        IBank newBank = _bankFactory.Create(bankInfo, this, _timeBroadcaster, additionalProxy);
        _banks.Add(newBank);
        return newBank;
    }

    public ITransaction TransitMoneyBetweenBanks(IAccount sender, IAccount catcher, TransactionValue amount)
    {
        if (sender.Bank == catcher.Bank)
        {
            throw CentralBankException.InnerBankTransaction(sender.Id, catcher.Id);
        }

        ITransaction transaction = new OneTimeReversableTransaction(
            () =>
            {
                sender.SubtractMoney(amount);
                catcher.AddMoney(amount);
            },
            () =>
            {
                sender.RevertSubtractingMoney(amount);
                catcher.RevertAddingMoney(amount);
            });

        transaction.Perform();
        return transaction;
    }
}
