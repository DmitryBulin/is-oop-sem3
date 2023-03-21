using Domain.Common.Exceptions;

namespace Domain.Messages;

public readonly record struct MessageCount
{
    public MessageCount(int value)
    {
        if (value < 0)
        {
            throw MessageCountException.InvalidValue(value);
        }

        Value = value;
    }

    public int Value { get; }

    public override string ToString() => Value.ToString();
}
