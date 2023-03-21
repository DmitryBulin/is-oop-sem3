namespace Isu.Exceptions;

public class InvalidStudentIdException : DomainException
{
    public InvalidStudentIdException()
        : base("Failed to get new id for student")
    {
    }
}
