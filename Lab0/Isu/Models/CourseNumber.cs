using Isu.Exceptions;

namespace Isu.Models;

public record CourseNumber
{
    private const int MinCourseNumber = 1;
    private const int MaxCourseNumber = 8;

    public CourseNumber(int course)
    {
        if (course >= MinCourseNumber && course <= MaxCourseNumber)
        {
            Course = course;
        }
        else
        {
            throw new InvalidCourseNumberException(course);
        }
    }

    public int Course { get; }
}
