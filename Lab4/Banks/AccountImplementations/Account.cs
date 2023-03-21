using Banks.Bank;
using Banks.Client;
using Banks.Transaction;

namespace Banks.Account;

public class Account : IAccount
{
    public Account(IClient owner, IBank bank, IAccountTerms accountTerms, DateOnly creationDate)
    {
        Owner = owner;
        Bank = bank;
        AccountTerms = accountTerms;
        CreationDate = creationDate;
    }

    public Guid Id { get; } = Guid.NewGuid();

    public IClient Owner { get; }

    public IBank Bank { get; }

    public IAccountTerms AccountTerms { get; private set; }

    public decimal Balance { get; private set; } = 0;

    public DateOnly CreationDate { get; }

    public void AddMoney(TransactionValue amount) => Balance += amount.Value;

    public void RevertAddingMoney(TransactionValue amount) => Balance -= amount.Value;

    public void SubtractMoney(TransactionValue amount) => Balance -= amount.Value;

    public void RevertSubtractingMoney(TransactionValue amount) => Balance += amount.Value;

    public void UpdateTerms(IAccountTerms newTerms) => AccountTerms = newTerms;

    public override string ToString() => $"{Id} - Owner: {Owner.Info.Id} Bank: {Bank.Info.Id} Balance: {Balance}";
}
