using Banks.Account;
using Banks.Bank;
using Banks.CentralBank;
using Banks.Client;
using Banks.Transaction;
using Banks.UpdateChannel;

namespace Banks.Console.Context;

public class Context : IContext
{
    public Context(ITimeUpdateChannel timeChannel)
    {
        TimeChannel = timeChannel;
    }

    public ICentralBank? CentralBank { get; set; }
    public IBankFactory? BankFactory { get; set; }
    public ITimeUpdateChannel TimeChannel { get; set; }
    public List<IBank> Banks { get; } = new List<IBank>();
    public List<IClient> Clients { get; } = new List<IClient>();
    public List<IAccount> Accounts { get; } = new List<IAccount>();
    public List<ITransaction> Transactions { get; } = new List<ITransaction>();
    public AccountCreator.AccountCreatorBuilder AccountCreatorBuilder { get; set; } = Account.AccountCreator.Builder;
    public int InterestPeriod { get; set; }
    public IAccountCreator? AccountCreator { get; set; }
    public IClientNameBuilder? ClientNameBuilder { get; set; }
    public IClientSecondNameBuilder? ClientSecondNameBuilder { get; set; }
    public IClientInfoBuilder? ClientInfoBuilder { get; set; }
    public IAccountProxyCreator TopProxy { get; set; } = new EmptyProxyCreator();
    public string BankName { get; set; } = string.Empty;
}
