using Banks.Account;
using Banks.CentralBank;
using Banks.Client;
using Banks.Exceptions;
using Banks.Maintenance;
using Banks.Transaction;
using Banks.UpdateChannel;

namespace Banks.Bank;

public class Bank : IBank
{
    private readonly IAccountCreator _accountCreator;
    private readonly IAccountProxyCreator _additionalProxy;
    private readonly IMaintenanceTask _maintenanceTask;
    private readonly ICentralBank _centralBank;
    private readonly List<IAccount> _accounts = new List<IAccount>();
    private readonly List<IAccountTerms> _terms = new List<IAccountTerms>();
    private DateTime _currentTime;

    public Bank(
        BankInfo info,
        IAccountCreator accountCreator,
        ICentralBank centralBank,
        ITimeUpdateChannel timeBroadcaster,
        IAccountProxyCreator additionalProxy,
        IMaintenanceTask maintenanceTask,
        ITermUpdateChannel termUpdateChannel)
    {
        _accountCreator = accountCreator;
        Info = info;
        _centralBank = centralBank;
        _currentTime = timeBroadcaster.CurrentTime;
        timeBroadcaster.Subscribe(UpdateTime);
        _additionalProxy = additionalProxy;
        _maintenanceTask = maintenanceTask;
        TermUpdate = termUpdateChannel;
    }

    public BankInfo Info { get; }

    public ITermUpdateChannel TermUpdate { get; }
    public IReadOnlyList<IAccountTerms> AvailableTerms => _terms;

    public IReadOnlyList<IAccount> OpenAccounts => _accounts;

    public IAccount OpenAccount(IClient client, IAccountTerms terms)
    {
        if (!_terms.Contains(terms))
        {
            throw BankException.AccountTermsAbsence();
        }

        IAccount account = _accountCreator.CreateAccount(this, terms, client, _additionalProxy, _currentTime);
        client.AddAccount(account);

        _accounts.Add(account);
        return account;
    }

    public void CloseAccount(IAccount account)
    {
        if (!_accounts.Contains(account))
        {
            throw BankException.AccountTermsAbsence();
        }

        account.Owner.RemoveAccount(account);
        _accounts.Remove(account);
    }

    public ITransaction TransitMoney(IAccount sender, IAccount catcher, TransactionValue amount)
    {
        if (sender.Bank != this)
        {
            throw BankException.AccountFromAnotherBank(Info.Id, sender.Bank.Info.Id);
        }

        if (catcher.Bank != this)
        {
            return _centralBank.TransitMoneyBetweenBanks(sender, catcher, amount);
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

    public void UpdateTerms(IReadOnlyList<AccountTermsUpdatePair> terms)
    {
        foreach (AccountTermsUpdatePair pair in terms)
        {
            if (_terms.Remove(pair.OldTerms))
            {
                _terms.Add(pair.NewTerms);
                foreach (IAccount account in _accounts.Where(account => pair.OldTerms == account.AccountTerms))
                {
                    account.UpdateTerms(pair.NewTerms);
                }
            }
            else
            {
                throw BankException.AccountTermsAbsence();
            }
        }

        TermUpdate.Notify(terms);
    }

    public void AddNewTerms(IReadOnlyList<IAccountTerms> terms)
    {
        if (terms.Any(term => _terms.Contains(term)))
        {
            throw BankException.AccountTermsDuplication();
        }

        _terms.AddRange(terms);
        TermUpdate.Notify(terms);
    }

    public void DeleteTerms(IReadOnlyList<IAccountTerms> terms)
    {
        if (terms.Any(term => !_terms.Contains(term)))
        {
            throw BankException.AccountTermsAbsence();
        }

        _terms.RemoveAll(term => terms.Contains(term));
    }

    private void UpdateTime(DateTime newDate)
    {
        _currentTime = newDate;

        int daysPassed = DateOnly.FromDateTime(newDate).DayNumber - DateOnly.FromDateTime(_currentTime).DayNumber;
        _accounts.ForEach(account => _maintenanceTask.Maintain(account, daysPassed));
    }
}
