using Shops.Exceptions;

namespace Shops.Entities;

public class BankAccount : IBalance
{
    public BankAccount(decimal balance = 0)
    {
        Earn(balance);
    }

    public decimal CurrentBalance { get; private set; }

    public void Spend(decimal payout)
    {
        if (payout > CurrentBalance)
        {
            throw BalanceException.OverspendException(CurrentBalance, payout);
        }

        if (payout < 0)
        {
            throw BalanceException.NegativeOperation(payout);
        }

        CurrentBalance -= payout;
    }

    public void Earn(decimal income)
    {
        if (income < 0)
        {
            throw BalanceException.NegativeOperation(income);
        }

        CurrentBalance += income;
    }
}
