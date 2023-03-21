using Banks.Console.Comand;
using Banks.Console.Context;
using Banks.Console.State;
using Banks.UpdateChannel;

ITimeUpdateChannel timeChannel = new TimeUpdateChannel();
timeChannel.Notify(DateTime.Today);

IContext context = new Context(timeChannel);

IState mainState = new State(ComandParser.Builder("help")
    .WithSubHandler(ComandParser.Builder("factory")
        .WithSubHandler(new ArgumentProxy("new", new FactoryNewComand()))
        .WithSubHandler(ComandParser.Builder("add proxy")
            .WithSubHandler(new ArgumentProxy<decimal, decimal>("verify", new FactoryAddProxyVerifyComand()))
            .Build())
        .WithSubHandler(new ArgumentProxy<int>("interest period", new FactoryInterestPeriodComand()))
        .WithSubHandler(new ArgumentProxy("build", new FactoryBuildComand()))
        .WithSubHandler(new ArgumentProxy("apply", new FactoryApplyComand()))
        .Build())
    .WithSubHandler(ComandParser.Builder("list")
        .WithSubHandler(new ArgumentProxy("banks", new ListBanksComand()))
        .WithSubHandler(new ArgumentProxy("clients", new ListClientsComand()))
        .WithSubHandler(new ArgumentProxy("accounts", new ListAccountsComand()))
        .WithSubHandler(new ArgumentProxy("transactions", new ListTransactionsComand()))
        .Build())
    .WithSubHandler(ComandParser.Builder("transaction")
        .WithSubHandler(new ArgumentProxy<Guid, Guid, decimal>("new", new TransactionNewComand()))
        .WithSubHandler(new ArgumentProxy<Guid>("revert", new TransactionRevertComand()))
        .Build())
    .WithSubHandler(ComandParser.Builder("client")
        .WithSubHandler(new ArgumentProxy("new", new ClientNewComand()))
        .WithSubHandler(ComandParser.Builder("with")
            .WithSubHandler(new ArgumentProxy<string>("name", new ClientWithNameComand()))
            .WithSubHandler(new ArgumentProxy<string>("second name", new ClientWithSecondNameComand()))
            .WithSubHandler(new ArgumentProxy<string>("address", new ClientWithAddressComand()))
            .WithSubHandler(new ArgumentProxy<string>("passport", new ClientWithPassportComand()))
            .Build())
        .WithSubHandler(new ArgumentProxy("build", new ClientBuildComand()))
        .WithSubHandler(ComandParser.Builder("update")
            .WithSubHandler(new ArgumentProxy<Guid, string>("name", new ClientUpdateNameComand()))
            .WithSubHandler(new ArgumentProxy<Guid, string>("second name", new ClientUpdateSecondNameComand()))
            .WithSubHandler(new ArgumentProxy<Guid, string>("address", new ClientUpdateAddressComand()))
            .WithSubHandler(new ArgumentProxy<Guid, string>("passport", new ClientUpdatePassportComand()))
            .Build())
        .Build())
    .WithSubHandler(ComandParser.Builder("bank")
        .WithSubHandler(new ArgumentProxy("new", new BankNewComand()))
        .WithSubHandler(ComandParser.Builder("with")
            .WithSubHandler(new ArgumentProxy<string>("name", new BankWithNameComand()))
            .WithSubHandler(ComandParser.Builder("proxy")
                .WithSubHandler(new ArgumentProxy<decimal, decimal>("verify", new BankWithProxyVerifyComand()))
                .Build())
            .Build())
        .WithSubHandler(new ArgumentProxy("build", new BankBuildComand()))
        .WithSubHandler(new ArgumentProxy<Guid, bool>("delete", new BankDeleteComand()))
        .WithSubHandler(ComandParser.Builder("terms")
            .WithSubHandler(new ArgumentProxy<Guid>("list", new BankTermsListComand()))
            .WithSubHandler(ComandParser.Builder("add")
                .WithSubHandler(new ArgumentProxy<Guid, decimal, decimal>("credit", new BankTermsAddCreditComand()))
                .WithSubHandler(new ArgumentProxy<Guid, decimal>("debit", new BankTermsAddDebitComand()))
                .Build())
            .WithSubHandler(new ArgumentProxy<Guid, int>("delete", new BankTermsDeleteComand()))
            .WithSubHandler(ComandParser.Builder("update")
                .WithSubHandler(new ArgumentProxy<Guid, int, decimal, decimal>("credit", new BankTermsUpdateCreditComand()))
                .WithSubHandler(new ArgumentProxy<Guid, int, decimal>("debit", new BankTermsUpdateDebitComand()))
                .Build())
            .Build())
        .Build())
    .WithSubHandler(ComandParser.Builder("account")
        .WithSubHandler(new ArgumentProxy<Guid, Guid, int>("new", new AccountNewComand()))
        .WithSubHandler(new ArgumentProxy<Guid>("delete", new AccountDeleteComand()))
        .Build())
    .WithSubHandler(ComandParser.Builder("time")
        .WithSubHandler(new ArgumentProxy("now", new TimeNowComand()))
        .WithSubHandler(new ArgumentProxy<int>("add", new TimeAddComand()))
        .Build())
    .WithSubHandler(new ArgumentProxy("exit", new ExitComand()))
    .Build());

IState initState = new State(ComandParser.Builder("help")
    .WithSubHandler(ComandParser.Builder("factory")
        .WithSubHandler(new ArgumentProxy("new", new FactoryNewComand()))
        .WithSubHandler(ComandParser.Builder("add proxy")
            .WithSubHandler(new ArgumentProxy<decimal, decimal>("verify", new FactoryAddProxyVerifyComand()))
            .Build())
        .WithSubHandler(new ArgumentProxy<int>("interest period", new FactoryInterestPeriodComand()))
        .WithSubHandler(new ArgumentProxy("build", new FactoryBuildComand()))
        .Build())
    .WithSubHandler(new ArgumentProxy("init", new InitComand(mainState)))
    .WithSubHandler(new ArgumentProxy("exit", new ExitComand()))
    .Build());

IStateMachine stateMachine = new StateMachine(context, initState);

Console.WriteLine("Welcome to Banks.Console. Enter 'help' to see available comands");

while (true)
{
    string input = Console.ReadLine() ?? string.Empty;
    try
    {
        string output = stateMachine.HandleInput(input);
        Console.WriteLine(output);
    }
    catch (Exception exception)
    {
        Console.WriteLine(exception.Message);
    }
}