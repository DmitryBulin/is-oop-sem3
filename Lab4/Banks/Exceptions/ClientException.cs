namespace Banks.Exceptions;

public class ClientException : BanksException
{
    private ClientException(string message)
        : base(message)
    {
    }

    public static ClientException WrongOwner(Guid client, Guid owner)
    {
        return new ClientException($"Tried to manage account of {owner} through {client}");
    }

    public static ClientException AccountDuplication(Guid client, Guid account)
    {
        return new ClientException($"Tried to add account {account} to client {client} multiple times");
    }

    public static ClientException AccountAbsence(Guid client, Guid account)
    {
        return new ClientException($"Tried to remove account {account} from client {client} that doesn't have it");
    }
}
