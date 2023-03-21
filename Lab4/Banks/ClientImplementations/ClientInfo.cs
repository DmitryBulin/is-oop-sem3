using Banks.Exceptions;

namespace Banks.Client;

public class ClientInfo : IClientInfo
{
    public ClientInfo(string name, string secondName, string? address, string? passportNumber)
    {
        Name = name;
        SecondName = secondName;
        Address = address;
        PassportNumber = passportNumber;
    }

    public static IClientNameBuilder Builder => new ClientInfoBuilder();
    public Guid Id { get; } = Guid.NewGuid();

    public string Name { get; private set; }

    public string SecondName { get; private set; }

    public string? Address { get; private set; }

    public string? PassportNumber { get; private set; }

    public void UpdateName(string name)
    {
        if (!ValidName(name))
        {
            throw ClientInfoException.InvalidName(name);
        }

        Name = name;
    }

    public void UpdateSecondName(string secondName)
    {
        if (!ValidSecondName(secondName))
        {
            throw ClientInfoException.InvalidSecondName(secondName);
        }

        SecondName = secondName;
    }

    public void UpdateAddress(string address)
    {
        if (!ValidAddress(address))
        {
            throw ClientInfoException.InvalidAddress(address);
        }

        Address = address;
    }

    public void UpdatePassportNumber(string passportNumber)
    {
        if (!ValidPassportName(passportNumber))
        {
            throw ClientInfoException.InvalidPassport(passportNumber);
        }

        PassportNumber = passportNumber;
    }

    public override string ToString() => $"{Id} - {Name} {SecondName} {PassportNumber} {Address}";

    private static bool ValidName(string name) => !string.IsNullOrEmpty(name);

    private static bool ValidSecondName(string secondName) => !string.IsNullOrEmpty(secondName);

    private static bool ValidAddress(string address) => !string.IsNullOrEmpty(address);

    private static bool ValidPassportName(string passportNumber) => !string.IsNullOrEmpty(passportNumber);

    public class ClientInfoBuilder : IClientNameBuilder, IClientSecondNameBuilder, IClientInfoBuilder
    {
        private string _name = string.Empty;
        private string _secondName = string.Empty;
        private string? _address;
        private string? _passportNumber;

        public IClientInfo Build()
        {
            return new ClientInfo(_name, _secondName, _address, _passportNumber);
        }

        public IClientSecondNameBuilder WithName(string name)
        {
            if (!ValidName(name))
            {
                throw ClientInfoException.InvalidName(name);
            }

            _name = name;
            return this;
        }

        public IClientInfoBuilder WithSecondName(string secondName)
        {
            if (!ValidSecondName(secondName))
            {
                throw ClientInfoException.InvalidSecondName(secondName);
            }

            _secondName = secondName;
            return this;
        }

        public IClientInfoBuilder WithAddress(string address)
        {
            if (!ValidAddress(address))
            {
                throw ClientInfoException.InvalidAddress(address);
            }

            _address = address;
            return this;
        }

        public IClientInfoBuilder WithPassportNumber(string passportNumber)
        {
            if (!ValidAddress(passportNumber))
            {
                throw ClientInfoException.InvalidPassport(passportNumber);
            }

            _passportNumber = passportNumber;
            return this;
        }
    }
}
