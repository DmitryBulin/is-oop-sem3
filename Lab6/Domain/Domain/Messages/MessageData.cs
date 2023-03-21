namespace Domain.Messages;

public readonly record struct MessageData
{
    public MessageData(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            value = string.Empty;
        }

        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}
