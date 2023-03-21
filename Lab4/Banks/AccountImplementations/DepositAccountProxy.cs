using Banks.Bank;
using Banks.Client;
using Banks.Exceptions;
using Banks.Transaction;
using Banks.UpdateChannel;

namespace Banks.Account;

public class DepositAccountProxy : IAccount
{
    private readonly IAccount _target;
    private readonly ITimeUpdateChannel _timeBroadcaster;
    private IDepositAccountTerms _depositTerms;

    public DepositAccountProxy(IAccount target, ITimeUpdateChannel timeBroadcaster, IDepositAccountTerms depositTerms)
    {
        _target = target;
        _timeBroadcaster = timeBroadcaster;
        _depositTerms = depositTerms;
    }

    public Guid Id => _target.Id;

    public IClient Owner => _target.Owner;

    public IBank Bank => _target.Bank;

    public IAccountTerms AccountTerms => _target.AccountTerms;

    public DateOnly CreationDate => _target.CreationDate;

    public decimal Balance => _target.Balance;

    public void AddMoney(TransactionValue amount) => _target.AddMoney(amount);

    public void RevertAddingMoney(TransactionValue amount) => _target.RevertAddingMoney(amount);

    public void RevertSubtractingMoney(TransactionValue amount) => _target.RevertSubtractingMoney(amount);

    public void SubtractMoney(TransactionValue amount)
    {
        if (DateOnly.FromDateTime(_timeBroadcaster.CurrentTime).DayNumber - CreationDate.DayNumber < _depositTerms.FreezeDaysCount)
        {
            throw DepositAccountException.SubtractingBeforeDeadline(Id);
        }

        _target.SubtractMoney(amount);
    }

    public void UpdateTerms(IAccountTerms newTerms)
    {
        if (newTerms is not IDepositAccountTerms)
        {
            throw AccountException.AccountTermsTypeChange(Id);
        }

        _depositTerms = (IDepositAccountTerms)newTerms;
        _target.UpdateTerms(newTerms);
    }
}