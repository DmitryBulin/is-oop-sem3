namespace Isu.Exceptions;
public class InvalidCourseNumberException : DomainException
{
    public InvalidCourseNumberException(int courseNumber)
        : base($"Failed to create CourseNumber from invalid {courseNumber}")
    {
    }
}
