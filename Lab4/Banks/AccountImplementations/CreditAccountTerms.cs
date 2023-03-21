namespace Banks.Account;

public class CreditAccountTerms : ICreditAccountTerms
{
    public CreditAccountTerms(decimal creditLimit, decimal overdraftCommission)
    {
        CreditLimit = creditLimit;
        OverdraftCommission = overdraftCommission;
    }

    public decimal CreditLimit { get; }

    public decimal OverdraftCommission { get; }

    public void Accept(IAccountTermsVisiter visiter) => visiter.Visit(this);

    public IAccount Wrap(IAccount account) => new CreditAccountProxy(account, this);

    public override string ToString() => $"Credit account. Limit: {CreditLimit} Overdraft commission:{OverdraftCommission}";
}
