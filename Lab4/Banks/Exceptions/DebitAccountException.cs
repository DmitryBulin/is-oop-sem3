namespace Banks.Exceptions;

public class DebitAccountException : BanksException
{
    private DebitAccountException(string message)
        : base(message)
    {
    }

    public static DebitAccountException NegativeBalanceAttempt(Guid account)
    {
        return new DebitAccountException($"Tried to get to negative balance on debit account {account}");
    }
}
