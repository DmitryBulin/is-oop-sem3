using Banks.Bank;
using Banks.Client;
using Banks.Exceptions;
using Banks.Transaction;

namespace Banks.Account;

public class CreditAccountProxy : IAccount
{
    private readonly IAccount _target;
    private ICreditAccountTerms _creditTerms;

    public CreditAccountProxy(IAccount target, ICreditAccountTerms creditTerms)
    {
        _target = target;
        _creditTerms = creditTerms;
    }

    public Guid Id => _target.Id;

    public IClient Owner => _target.Owner;

    public IBank Bank => _target.Bank;

    public IAccountTerms AccountTerms => _target.AccountTerms;

    public DateOnly CreationDate => _target.CreationDate;

    public decimal Balance => _target.Balance;

    public void AddMoney(TransactionValue amount) => _target.AddMoney(amount);

    public void RevertAddingMoney(TransactionValue amount) => _target.RevertAddingMoney(amount);

    public void RevertSubtractingMoney(TransactionValue amount)
    {
        if (Balance + amount.Value + _creditTerms.OverdraftCommission < -_creditTerms.CreditLimit)
        {
            _target.RevertSubtractingMoney(new TransactionValue(_creditTerms.OverdraftCommission));
        }

        _target.RevertSubtractingMoney(amount);
    }

    public void SubtractMoney(TransactionValue amount)
    {
        if (Balance < -_creditTerms.CreditLimit)
        {
            _target.SubtractMoney(new TransactionValue(_creditTerms.OverdraftCommission));
        }

        _target.SubtractMoney(amount);
    }

    public void UpdateTerms(IAccountTerms newTerms)
    {
        if (newTerms is not ICreditAccountTerms)
        {
            throw AccountException.AccountTermsTypeChange(Id);
        }

        _creditTerms = (ICreditAccountTerms)newTerms;
        _target.UpdateTerms(newTerms);
    }
}