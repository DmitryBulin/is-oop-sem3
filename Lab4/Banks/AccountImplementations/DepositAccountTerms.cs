using Banks.UpdateChannel;

namespace Banks.Account;

public class DepositAccountTerms : IDepositAccountTerms
{
    private readonly ITimeUpdateChannel _timeChannel;
    private readonly IReadOnlyCollection<KeyValuePair<decimal, decimal>> _interestPercentage;

    public DepositAccountTerms(int freezeDaysCount, IReadOnlyCollection<KeyValuePair<decimal, decimal>> interestPercentage, ITimeUpdateChannel timeChannel)
    {
        FreezeDaysCount = freezeDaysCount;
        _interestPercentage = interestPercentage;
        _timeChannel = timeChannel;
    }

    public int FreezeDaysCount { get; }

    public void Accept(IAccountTermsVisiter visiter) => visiter.Visit(this);

    public IAccount Wrap(IAccount account) => new DepositAccountProxy(account, _timeChannel, this);

    decimal IDepositAccountTerms.InterestPercentage(decimal balance)
    {
        return _interestPercentage.FirstOrDefault(pair => pair.Key >= balance, _interestPercentage.Last()).Value;
    }
}
