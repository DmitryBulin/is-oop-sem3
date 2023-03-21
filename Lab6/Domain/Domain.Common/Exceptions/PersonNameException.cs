namespace Domain.Common.Exceptions;

public class PersonNameException : DomainException
{
    private PersonNameException(string? message)
        : base(message) { }

    public static PersonNameException InvalidValue(string value)
        => new PersonNameException($"Invalid value: {value}");
}
