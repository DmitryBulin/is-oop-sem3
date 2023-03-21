using Isu.Entities;
using Isu.Models;

namespace Isu.Exceptions;

public class GroupCapacityException : DomainException
{
    public GroupCapacityException(GroupName name, int initialCapacity, int newCapacity)
        : this($"Failed to change capacity for group {name} from {initialCapacity} to {newCapacity}")
    {
    }

    public GroupCapacityException(int newCapacity, int studentsCount, GroupName name)
        : this($"Failed to change capacity for group {name} since {newCapacity} is less then students in group: {studentsCount}")
    {
    }

    public GroupCapacityException(GroupName name, Student student)
        : this($"Failed to add student {student.Id} to group {name} since it already has max students")
    {
    }

    private GroupCapacityException(string message)
        : base(message)
    {
    }
}
