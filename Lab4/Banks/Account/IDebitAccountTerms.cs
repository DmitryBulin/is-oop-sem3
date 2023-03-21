namespace Banks.Account;

public interface IDebitAccountTerms : IAccountTerms
{
    decimal InterestPercentage { get; }
}