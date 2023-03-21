namespace Banks.Exceptions;

public class AccountException : BanksException
{
    private AccountException(string message)
        : base(message)
    {
    }

    public static AccountException UnverifiedLimitExceeded(Guid account)
    {
        return new AccountException($"Tried to exceed limits for unverified account {account}");
    }

    public static AccountException AccountTermsTypeChange(Guid account)
    {
        return new AccountException($"Tried to change terms type for account {account}");
    }

    public static AccountException NullAccountOperation()
    {
        return new AccountException("Trying to perform operation on placeholder account");
    }
}
