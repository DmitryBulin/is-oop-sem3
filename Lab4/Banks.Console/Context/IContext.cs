using Banks.Account;
using Banks.Bank;
using Banks.CentralBank;
using Banks.Client;
using Banks.Transaction;
using Banks.UpdateChannel;

namespace Banks.Console.Context;

public interface IContext
{
    AccountCreator.AccountCreatorBuilder AccountCreatorBuilder { get; set; }
    int InterestPeriod { get; set; }
    IAccountCreator? AccountCreator { get; set; }
    IBankFactory? BankFactory { get; set; }

    IClientNameBuilder? ClientNameBuilder { get; set; }
    IClientSecondNameBuilder? ClientSecondNameBuilder { get; set; }
    IClientInfoBuilder? ClientInfoBuilder { get; set; }

    IAccountProxyCreator TopProxy { get; set; }
    string BankName { get; set; }

    ICentralBank? CentralBank { get; set; }
    ITimeUpdateChannel TimeChannel { get; set; }
    List<IBank> Banks { get; }
    List<IClient> Clients { get; }
    List<IAccount> Accounts { get; }
    List<ITransaction> Transactions { get; }
}
