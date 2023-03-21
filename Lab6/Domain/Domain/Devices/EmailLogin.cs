using Domain.Common.Exceptions;

namespace Domain.Devices;

public readonly record struct EmailLogin
{
    public EmailLogin(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw EmailLoginException.InvalidValue(value);
        }

        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}
