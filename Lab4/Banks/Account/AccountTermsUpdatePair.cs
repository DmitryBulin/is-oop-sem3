namespace Banks.Account;

public class AccountTermsUpdatePair
{
    public AccountTermsUpdatePair(IAccountTerms oldTerms, IAccountTerms newTerms)
    {
        OldTerms = oldTerms;
        NewTerms = newTerms;
    }

    public IAccountTerms OldTerms { get; }
    public IAccountTerms NewTerms { get; }
}
