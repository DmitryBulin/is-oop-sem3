namespace Isu.Exceptions;

public class InvalidStudentNameException : DomainException
{
    public InvalidStudentNameException(string studentName, int studentId)
        : base($"Failed to assign invalid name {studentName} to a student {studentId}")
    {
    }
}
