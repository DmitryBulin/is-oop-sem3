namespace Banks.Exceptions;

public class TransactionException : BanksException
{
    private TransactionException(string message)
        : base(message)
    {
    }

    public static TransactionException NonPositiveValue(decimal value)
    {
        return new TransactionException($"Tried to create transaction value with non positive {value}");
    }

    public static TransactionException PerformRepeat(Guid id)
    {
        return new TransactionException($"Tried to perform transaction {id} more times than possible");
    }

    public static TransactionException RevertBeforePerform(Guid id)
    {
        return new TransactionException($"Tried to revert transaction {id} before performing it");
    }

    public static TransactionException RevertRepeat(Guid id)
    {
        return new TransactionException($"Tried to revert transaction {id} more times than possible");
    }
}
