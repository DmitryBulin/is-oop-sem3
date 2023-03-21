namespace Isu.Exceptions;

public class InvalidGroupNameException : DomainException
{
    public InvalidGroupNameException(string name)
        : base($"Failed to assign name {name} to group")
    {
    }
}
