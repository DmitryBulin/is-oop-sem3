namespace Shops.Exceptions;

public class BalanceException : ShopsException
{
    private BalanceException(string message)
        : base(message)
    {
    }

    public static BalanceException NoAbilityToSpendExcpetion()
    {
        return new BalanceException("Can't spend from this balance");
    }

    public static BalanceException OverspendException(decimal currentBalance, decimal spendAmount)
    {
        return new BalanceException($"Can't spend {currentBalance}, since there is only {spendAmount} on this balance");
    }

    public static BalanceException NegativeOperation(decimal balance)
    {
        return new BalanceException($"Tried to change balance with non positive {balance}");
    }
}
