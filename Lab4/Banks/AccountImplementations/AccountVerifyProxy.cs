using Banks.Bank;
using Banks.Client;
using Banks.Exceptions;
using Banks.Transaction;

namespace Banks.Account;

public class AccountVerifyProxy : IAccount
{
    private readonly IClientVerifier _clientVerifier;
    private readonly IAccount _target;
    private readonly decimal _maxAddAmount;
    private readonly decimal _maxSubtractAmount;

    public AccountVerifyProxy(IClientVerifier clientVerifier, IAccount target, decimal maxAddAmount, decimal maxSubtractAmount)
    {
        _clientVerifier = clientVerifier;
        _target = target;
        _maxAddAmount = maxAddAmount;
        _maxSubtractAmount = maxSubtractAmount;
    }

    public Guid Id => _target.Id;

    public IClient Owner => _target.Owner;

    public IBank Bank => _target.Bank;

    public IAccountTerms AccountTerms => _target.AccountTerms;

    public decimal Balance => _target.Balance;

    public DateOnly CreationDate => _target.CreationDate;

    public void UpdateTerms(IAccountTerms newTerms) => _target.UpdateTerms(newTerms);

    public void AddMoney(TransactionValue amount)
    {
        if (!_clientVerifier.Verify(Owner) && amount.Value > _maxAddAmount)
        {
            throw AccountException.UnverifiedLimitExceeded(Id);
        }

        _target.AddMoney(amount);
    }

    public void SubtractMoney(TransactionValue amount)
    {
        if (!_clientVerifier.Verify(Owner) && amount.Value > _maxSubtractAmount)
        {
            throw AccountException.UnverifiedLimitExceeded(Id);
        }

        _target.SubtractMoney(amount);
    }

    public void RevertAddingMoney(TransactionValue amount) => _target.RevertAddingMoney(amount);

    public void RevertSubtractingMoney(TransactionValue amount) => _target.RevertSubtractingMoney(amount);
}
