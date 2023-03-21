namespace Domain.Common.Exceptions;

public class PasswordException : DomainException
{
    private PasswordException(string? message)
        : base(message) { }

    public static PasswordException InvalidValue(string value)
        => new PasswordException($"Invalid value: {value}");
}
