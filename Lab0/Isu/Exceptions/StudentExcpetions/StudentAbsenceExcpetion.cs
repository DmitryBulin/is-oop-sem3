namespace Isu.Exceptions;

public class StudentAbsenceException : DomainException
{
    public StudentAbsenceException(int studentId)
        : base($"Failed to get student with id {studentId}")
    {
    }
}
