using Domain.Common.Exceptions;

namespace Domain.Devices;

public readonly record struct PhoneNumber
{
    public PhoneNumber(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw PhoneNumberException.InvalidValue(value);
        }

        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}
