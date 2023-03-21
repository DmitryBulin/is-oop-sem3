namespace Banks.Client;

public class ClientVerifier : IClientVerifier
{
    public bool Verify(IClient client)
    {
        return client.Info.Address is not null && client.Info.PassportNumber is not null;
    }
}
