using Banks.Exceptions;

namespace Banks.Bank;

public record BankInfo : IEquatable<BankInfo>
{
    public BankInfo(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw BankInfoException.InvalidName(name);
        }

        Name = name;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }

    public override string ToString() => $"{Id} - {Name}";
}