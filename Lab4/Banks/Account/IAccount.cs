using Banks.Bank;
using Banks.Client;
using Banks.Transaction;

namespace Banks.Account;

public interface IAccount
{
    Guid Id { get; }
    IClient Owner { get; }
    IBank Bank { get; }
    IAccountTerms AccountTerms { get; }
    DateOnly CreationDate { get; }
    decimal Balance { get; }
    void UpdateTerms(IAccountTerms newTerms);
    void AddMoney(TransactionValue amount);
    void RevertAddingMoney(TransactionValue amount);
    void SubtractMoney(TransactionValue amount);
    void RevertSubtractingMoney(TransactionValue amount);
}