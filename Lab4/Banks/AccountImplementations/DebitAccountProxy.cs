using Banks.Bank;
using Banks.Client;
using Banks.Exceptions;
using Banks.Transaction;

namespace Banks.Account;

public class DebitAccountProxy : IAccount
{
    private readonly IAccount _target;

    public DebitAccountProxy(IAccount target)
    {
        _target = target;
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
        if (Balance < amount.Value)
        {
            throw DebitAccountException.NegativeBalanceAttempt(Id);
        }

        _target.SubtractMoney(amount);
    }

    public void UpdateTerms(IAccountTerms newTerms)
    {
        if (newTerms is not IDebitAccountTerms)
        {
            throw AccountException.AccountTermsTypeChange(Id);
        }

        _target.UpdateTerms(newTerms);
    }
}