using Banks.Exceptions;

namespace Banks.Transaction;

public record TransactionValue
{
    public TransactionValue(decimal value)
    {
        if (value <= 0)
        {
            throw TransactionException.NonPositiveValue(value);
        }

        Value = value;
    }

    public decimal Value { get; }
}
