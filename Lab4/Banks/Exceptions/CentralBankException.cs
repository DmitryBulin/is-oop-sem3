namespace Banks.Exceptions;

public class CentralBankException : BanksException
{
    private CentralBankException(string message)
        : base(message)
    {
    }

    public static CentralBankException BankDeleteAccountsExist(Guid bank)
    {
        return new CentralBankException($"Tried to delete bank {bank} that has open accounts");
    }

    public static CentralBankException BankAbsence(Guid bank)
    {
        return new CentralBankException($"Failed to find bank {bank}");
    }

    public static CentralBankException BankDuplication(Guid bank)
    {
        return new CentralBankException($"Tried to create bank {bank} multiple times");
    }

    public static CentralBankException InnerBankTransaction(Guid sender, Guid catcher)
    {
        return new CentralBankException($"Tried to transfer money between banks, but accounts {sender} and {catcher} registered in the same bank");
    }
}
