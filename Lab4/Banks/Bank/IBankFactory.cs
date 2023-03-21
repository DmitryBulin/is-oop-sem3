using Banks.Account;
using Banks.CentralBank;
using Banks.UpdateChannel;

namespace Banks.Bank;

public interface IBankFactory
{
    IBank Create(BankInfo bankInfo, ICentralBank centralBank, ITimeUpdateChannel timeBroadcaster, IAccountProxyCreator additionalProxy);
}
