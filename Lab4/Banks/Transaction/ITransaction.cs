namespace Banks.Transaction;

public interface ITransaction
{
    Guid Id { get; }
    void Perform();
    void Revert();
}