namespace Domain.Common.Exceptions;

public class PhoneNumberException : DomainException
{
    private PhoneNumberException(string? message)
        : base(message) { }

    public static PhoneNumberException InvalidValue(string value)
        => new PhoneNumberException($"Invalid value: {value}");
}
