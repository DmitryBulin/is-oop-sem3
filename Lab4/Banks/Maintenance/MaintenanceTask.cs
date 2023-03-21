using Banks.Account;
using Banks.Transaction;

namespace Banks.Maintenance;

public class MaintenanceTask : IMaintenanceTask, IAccountTermsVisiter
{
    private readonly Dictionary<Guid, decimal> _accumulatedInterest = new Dictionary<Guid, decimal>();
    private readonly Dictionary<Guid, int> _daysCounted = new Dictionary<Guid, int>();
    private readonly int _interestPeriod;
    private IAccount _account = new NullAccount();

    public MaintenanceTask(int interestPeriod)
    {
        _interestPeriod = interestPeriod;
    }

    public void Maintain(IAccount account, int daysPassed)
    {
        _account = account;
        for (int i = 0; i < daysPassed; ++i)
        {
            account.AccountTerms.Accept(this);
        }
    }

    public void Visit(ICreditAccountTerms accountTerms)
    {
    }

    public void Visit(IDebitAccountTerms accountTerms)
    {
        CalculateInterest(accountTerms.InterestPercentage);
    }

    public void Visit(IDepositAccountTerms accountTerms)
    {
        CalculateInterest(accountTerms.InterestPercentage(_account.Balance));
    }

    private void CalculateInterest(decimal interestRate)
    {
        _accumulatedInterest[_account.Id] += _account.Balance * interestRate;
        _daysCounted[_account.Id]++;

        if ((_daysCounted[_account.Id] + 1) % _interestPeriod == 0)
        {
            _account.AddMoney(new TransactionValue(_accumulatedInterest[_account.Id]));
            _accumulatedInterest[_account.Id] = 0;
            _daysCounted[_account.Id] = 0;
        }
    }
}
