using Isu.Models;

namespace Isu.Exceptions;

public class StudentDuplicationException : DomainException
{
    public StudentDuplicationException(int studentId, GroupName groupName)
        : base($"Failed to add student {studentId} in group {groupName} since it already contains this student")
    {
    }
}