namespace Banks.Exceptions;

public class ProxyCreatorException : BanksException
{
    private ProxyCreatorException(string message)
        : base(message)
    {
    }

    public static ProxyCreatorException DefaultProxyInMiddleOfChain()
    {
        return new ProxyCreatorException("Tried to access next chain element through default creator that must be on the tail");
    }
}
