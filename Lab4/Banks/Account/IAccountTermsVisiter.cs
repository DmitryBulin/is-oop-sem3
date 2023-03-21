namespace Banks.Account;

public interface IAccountTermsVisiter
{
    void Visit(ICreditAccountTerms accountTerms);
    void Visit(IDebitAccountTerms accountTerms);
    void Visit(IDepositAccountTerms accountTerms);
}
