using Isu.Models;

namespace Isu.Extra.Exceptions;

public class ExtraStudyServiceException : IsuExtraException
{
    private ExtraStudyServiceException(string message)
        : base(message)
    {
    }

    public static ExtraStudyServiceException StudentFromSameFacultyException(int studentId, GroupName extraStudyGroupName)
    {
        return new ExtraStudyServiceException($"Student {studentId} is from the same faculty as the Extra Study group {extraStudyGroupName}");
    }

    public static ExtraStudyServiceException StudentMaxExtraStudyExceededException(int studentId)
    {
        return new ExtraStudyServiceException($"Student {studentId} is already in max amount of Extra Study groups");
    }

    public static ExtraStudyServiceException ExtraStudyGroupLessonsIntersects(int studentId, GroupName extraStudyGroupName)
    {
        return new ExtraStudyServiceException($"Lessons of group {extraStudyGroupName} is intersecting with lessons of group of student {studentId}");
    }

    public static ExtraStudyServiceException NewScheduleIntersectLessons()
    {
        return new ExtraStudyServiceException("New schedule intersects with Extra Study lessons of at least one student");
    }
}
