namespace Banks.Client;

public interface IClientVerifier
{
    bool Verify(IClient client);
}
