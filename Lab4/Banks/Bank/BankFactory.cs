using Banks.Account;
using Banks.CentralBank;
using Banks.Maintenance;
using Banks.UpdateChannel;

namespace Banks.Bank;

public class BankFactory : IBankFactory
{
    private readonly IAccountCreator _accountCreator;
    private readonly int _interestPeriod;

    public BankFactory(IAccountCreator accountCreator, int interestPeriod)
    {
        _accountCreator = accountCreator;
        _interestPeriod = interestPeriod;
    }

    public IBank Create(BankInfo bankInfo, ICentralBank centralBank, ITimeUpdateChannel timeBroadcaster, IAccountProxyCreator additionalProxy)
    {
        return new Bank(
            bankInfo,
            _accountCreator,
            centralBank,
            timeBroadcaster,
            additionalProxy,
            new MaintenanceTask(_interestPeriod),
            new TermUpdateChannel());
    }
}
