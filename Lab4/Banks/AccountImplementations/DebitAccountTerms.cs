namespace Banks.Account;

public class DebitAccountTerms : IDebitAccountTerms
{
    public DebitAccountTerms(decimal interestPercentage)
    {
        InterestPercentage = interestPercentage;
    }

    public decimal InterestPercentage { get; }

    public void Accept(IAccountTermsVisiter visiter) => visiter.Visit(this);

    public IAccount Wrap(IAccount account) => new DebitAccountProxy(account);
    public override string ToString() => $"Debit account. Intereset percentage:{InterestPercentage}";
}
