using Isu.Models;

namespace Isu.Exceptions;

public class GroupDuplicationException : DomainException
{
    public GroupDuplicationException(GroupName name)
        : base($"Encountered duplication of the group {name}")
    {
    }
}