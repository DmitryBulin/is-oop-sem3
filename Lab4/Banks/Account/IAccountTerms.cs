namespace Banks.Account;

public interface IAccountTerms
{
    void Accept(IAccountTermsVisiter visiter);
    IAccount Wrap(IAccount account);
}