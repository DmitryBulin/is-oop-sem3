namespace Banks.Exceptions;

public class DepositAccountException : BanksException
{
    private DepositAccountException(string message)
        : base(message)
    {
    }

    public static DepositAccountException SubtractingBeforeDeadline(Guid account)
    {
        return new DepositAccountException($"Tried to get money from the account {account} before end of freeze period");
    }
}
