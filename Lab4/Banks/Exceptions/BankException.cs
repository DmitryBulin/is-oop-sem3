namespace Banks.Exceptions;

public class BankException : BanksException
{
    private BankException(string message)
        : base(message)
    {
    }

    public static BankException AccountTermsAbsence()
    {
        return new BankException($"Tried to access terms that bank doesn't have");
    }

    public static BankException AccountTermsDuplication()
    {
        return new BankException($"Tried to add terms that bank already have");
    }

    public static BankException AccountFromAnotherBank(Guid bank, Guid owner)
    {
        return new BankException($"Tried to send money from account through bank {bank} but he is from bank {owner}");
    }
}
