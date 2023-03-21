namespace Banks.Account;

public interface IDepositAccountTerms : IAccountTerms
{
    int FreezeDaysCount { get; }
    decimal InterestPercentage(decimal balance);
}