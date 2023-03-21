namespace Domain.Common.Exceptions;

public class MessageCountException : DomainException
{
    private MessageCountException(string? message)
        : base(message) { }

    public static MessageCountException InvalidValue(int value)
        => new MessageCountException($"Invalid value: {value}");
}
