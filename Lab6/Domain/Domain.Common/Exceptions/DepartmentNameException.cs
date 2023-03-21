namespace Domain.Common.Exceptions;

public class DepartmentNameException : DomainException
{
    private DepartmentNameException(string? message)
        : base(message) { }

    public static DepartmentNameException InvalidValue(string value)
        => new DepartmentNameException($"Invalid value: {value}");
}
