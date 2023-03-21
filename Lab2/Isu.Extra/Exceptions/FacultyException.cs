using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Exceptions;

public class FacultyException : IsuExtraException
{
    private FacultyException(string message)
        : base(message)
    {
    }

    public static FacultyException ExtraStudyAbcentException(ExtraStudyName extraStudyName, string facultyName)
    {
        return new FacultyException($"Tried to remove Extra Study {extraStudyName} that is not from the Faculty {facultyName}");
    }

    public static FacultyException GroupNameAbcentException(GroupName groupName, string facultyName)
    {
        return new FacultyException($"Tried to remove Group {groupName} that is not from the Faculty {facultyName}");
    }

    public static FacultyException ExtraStudyDuplicationException(ExtraStudyName extraStudyName, string facultyName)
    {
        return new FacultyException($"Tried to add duplicate of Extra Study {extraStudyName} to faculty {facultyName}");
    }
}
