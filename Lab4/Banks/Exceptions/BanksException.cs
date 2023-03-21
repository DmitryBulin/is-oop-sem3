namespace Banks.Exceptions;

public abstract class BanksException : Exception
{
    public BanksException(string message)
        : base(message)
    {
    }
}
