namespace Banks.Exceptions;

public class BankInfoException : BanksException
{
    private BankInfoException(string message)
        : base(message)
    {
    }

    public static BankInfoException InvalidName(string name)
    {
        return new BankInfoException($"Tried to set invalid name {name}");
    }
}
