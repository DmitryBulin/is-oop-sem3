using Isu.Extra.Models;

namespace Isu.Extra.Exceptions;

public class ScheduleException : IsuExtraException
{
    private ScheduleException(string message)
        : base(message)
    {
    }

    public static ScheduleException LessonDuplicationException(Lesson lesson)
    {
        return new ScheduleException($"Tried to add duplicate of the lesson {lesson}");
    }
}
