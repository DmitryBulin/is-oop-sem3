namespace Domain.Common.Exceptions;

public class EmailLoginException : DomainException
{
    private EmailLoginException(string? message)
        : base(message) { }

    public static EmailLoginException InvalidValue(string value)
        => new EmailLoginException($"Invalid value: {value}");
}
