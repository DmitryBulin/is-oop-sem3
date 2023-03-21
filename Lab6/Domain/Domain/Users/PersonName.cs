using Domain.Common.Exceptions;

namespace Domain.Users;

public readonly record struct PersonName
{
    public PersonName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw PersonNameException.InvalidValue(value);
        }

        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}
