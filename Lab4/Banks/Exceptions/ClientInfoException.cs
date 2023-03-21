namespace Banks.Exceptions;

public class ClientInfoException : BanksException
{
    private ClientInfoException(string message)
        : base(message)
    {
    }

    public static ClientInfoException InvalidName(string name)
    {
        return new ClientInfoException($"Tried to set invalid name {name}");
    }

    public static ClientInfoException InvalidSecondName(string secondName)
    {
        return new ClientInfoException($"Tried to set invalid second name {secondName}");
    }

    public static ClientInfoException InvalidAddress(string address)
    {
        return new ClientInfoException($"Tried to set invalid address {address}");
    }

    public static ClientInfoException InvalidPassport(string passport)
    {
        return new ClientInfoException($"Tried to set invalid passport number {passport}");
    }
}
