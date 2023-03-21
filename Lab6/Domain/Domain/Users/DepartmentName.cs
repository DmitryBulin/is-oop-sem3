using Domain.Common.Exceptions;

namespace Domain.Users;

public readonly record struct DepartmentName
{
    public DepartmentName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw DepartmentNameException.InvalidValue(value);
        }

        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}