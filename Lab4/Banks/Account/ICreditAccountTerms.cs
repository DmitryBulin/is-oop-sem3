namespace Banks.Account;

public interface ICreditAccountTerms : IAccountTerms
{
    decimal CreditLimit { get; }
    decimal OverdraftCommission { get; }
}