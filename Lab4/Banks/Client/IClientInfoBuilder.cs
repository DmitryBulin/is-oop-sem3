namespace Banks.Client;

public interface IClientInfoBuilder
{
    IClientInfoBuilder WithAddress(string address);
    IClientInfoBuilder WithPassportNumber(string passportNumber);
    IClientInfo Build();
}

public interface IClientNameBuilder
{
    IClientSecondNameBuilder WithName(string name);
}

public interface IClientSecondNameBuilder
{
    IClientInfoBuilder WithSecondName(string secondName);
}