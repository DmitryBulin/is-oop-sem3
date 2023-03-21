using Banks.Bank;
using Banks.Client;
using Banks.Exceptions;
using Banks.Transaction;

namespace Banks.Account;

public class NullAccount : IAccount
{
    public Guid Id => throw AccountException.NullAccountOperation();

    public IClient Owner => throw AccountException.NullAccountOperation();

    public IBank Bank => throw AccountException.NullAccountOperation();

    public IAccountTerms AccountTerms => throw AccountException.NullAccountOperation();

    public DateOnly CreationDate => throw AccountException.NullAccountOperation();

    public decimal Balance => throw AccountException.NullAccountOperation();

    public void AddMoney(TransactionValue amount) => throw AccountException.NullAccountOperation();

    public void RevertAddingMoney(TransactionValue amount) => throw AccountException.NullAccountOperation();

    public void RevertSubtractingMoney(TransactionValue amount) => throw AccountException.NullAccountOperation();

    public void SubtractMoney(TransactionValue amount) => throw AccountException.NullAccountOperation();

    public void UpdateTerms(IAccountTerms newTerms) => throw AccountException.NullAccountOperation();
}
