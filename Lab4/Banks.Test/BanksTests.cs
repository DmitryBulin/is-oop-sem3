using Banks.Account;
using Banks.Bank;
using Banks.CentralBank;
using Banks.Client;
using Banks.Transaction;
using Banks.UpdateChannel;
using Xunit;

namespace Banks.Test;

public class BanksTests
{
    [Fact]
    public void CreateBankUpdateTerms_TermsUpdated()
    {
        ITimeUpdateChannel timeChannel = new TimeUpdateChannel();
        IAccountCreator accountCreator = AccountCreator.Builder.Build();
        IBankFactory bankFactory = new BankFactory(accountCreator, 30);
        ICentralBank centralBank = new CentralBank.CentralBank(bankFactory, timeChannel);
        IBank bank = centralBank.RegisterNewBank(new BankInfo("Monopoly Bank"), new DefaultProxyCreator());
        IAccountTerms debitTerms = new DebitAccountTerms(0.5m);
        IAccountTerms creditTerms = new CreditAccountTerms(15000, 99);
        IAccountTerms depositTerms = new DepositAccountTerms(90, new List<KeyValuePair<decimal, decimal>>() { new KeyValuePair<decimal, decimal>(50000, .5m) }, timeChannel);
        var terms = new List<IAccountTerms>() { debitTerms, creditTerms, depositTerms };

        bank.AddNewTerms(terms);

        terms.ForEach(term => Assert.Contains(term, bank.AvailableTerms));
    }

    [Fact]
    public void CreateClientUnverified_ClientIsUnverified()
    {
        IClientInfo clientInfo = ClientInfo.Builder
            .WithName("Dmitry")
            .WithSecondName("Bulin")
            .Build();
        IClient client = new Client.Client(clientInfo);
        IClientVerifier clientVerifier = new ClientVerifier();

        Assert.False(clientVerifier.Verify(client));
    }

    [Fact]
    public void CreateAccount_AccountCreated()
    {
        IClientInfo clientInfo = ClientInfo.Builder
            .WithName("Dmitry")
            .WithSecondName("Bulin")
            .Build();
        IClient client = new Client.Client(clientInfo);
        ITimeUpdateChannel timeChannel = new TimeUpdateChannel();
        IAccountCreator accountCreator = AccountCreator.Builder.Build();
        IBankFactory bankFactory = new BankFactory(accountCreator, 30);
        ICentralBank centralBank = new CentralBank.CentralBank(bankFactory, timeChannel);
        IBank bank = centralBank.RegisterNewBank(new BankInfo("Monopoly Bank"), new DefaultProxyCreator());
        IAccountTerms term = new DebitAccountTerms(0.5m);

        bank.AddNewTerms(new List<IAccountTerms>() { term });
        IAccount account = bank.OpenAccount(client, term);

        Assert.Equal(client, account.Owner);
        Assert.Equal(bank, account.Bank);
        Assert.Equal(0, account.Balance);
        Assert.Equal(term, account.AccountTerms);
    }

    [Fact]
    public void TransferMoney_MoneyTransfered()
    {
        IClientInfo clientInfo = ClientInfo.Builder
            .WithName("Dmitry")
            .WithSecondName("Bulin")
            .Build();
        IClient client = new Client.Client(clientInfo);
        ITimeUpdateChannel timeChannel = new TimeUpdateChannel();
        IAccountCreator accountCreator = AccountCreator.Builder.Build();
        IBankFactory bankFactory = new BankFactory(accountCreator, 30);
        ICentralBank centralBank = new CentralBank.CentralBank(bankFactory, timeChannel);
        IBank bank = centralBank.RegisterNewBank(new BankInfo("Monopoly Bank"), new DefaultProxyCreator());
        IAccountTerms term = new CreditAccountTerms(15000, 99);

        bank.AddNewTerms(new List<IAccountTerms>() { term });
        IAccount firstAccount = bank.OpenAccount(client, term);
        IAccount secondAccount = bank.OpenAccount(client, term);

        bank.TransitMoney(firstAccount, secondAccount, new TransactionValue(100));

        Assert.Equal(100, secondAccount.Balance);
        Assert.Equal(-100, firstAccount.Balance);
    }

    [Fact]
    public void TransactionRevert_MoneyReverted()
    {
        IClientInfo clientInfo = ClientInfo.Builder
            .WithName("Dmitry")
            .WithSecondName("Bulin")
            .Build();
        IClient client = new Client.Client(clientInfo);
        ITimeUpdateChannel timeChannel = new TimeUpdateChannel();
        IAccountCreator accountCreator = AccountCreator.Builder.Build();
        IBankFactory bankFactory = new BankFactory(accountCreator, 30);
        ICentralBank centralBank = new CentralBank.CentralBank(bankFactory, timeChannel);
        IBank bank = centralBank.RegisterNewBank(new BankInfo("Monopoly Bank"), new DefaultProxyCreator());
        IAccountTerms term = new CreditAccountTerms(15000, 99);

        bank.AddNewTerms(new List<IAccountTerms>() { term });
        IAccount firstAccount = bank.OpenAccount(client, term);
        IAccount secondAccount = bank.OpenAccount(client, term);

        ITransaction transaction = bank.TransitMoney(firstAccount, secondAccount, new TransactionValue(100));
        transaction.Revert();

        Assert.Equal(0, firstAccount.Balance);
        Assert.Equal(0, secondAccount.Balance);
    }
}
