using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Exceptions;

public class ExtraStudyException : IsuExtraException
{
    private ExtraStudyException(string message)
        : base(message)
    {
    }

    public static ExtraStudyException GroupAbcentException(ExtraStudyName extraStudyName, GroupName groupName)
    {
        return new ExtraStudyException($"Tried to remove Extra Study Group {groupName} that is not from the Extra Study {extraStudyName}");
    }

    public static ExtraStudyException InvalidName(string name)
    {
        return new ExtraStudyException($"Tried to create name for Extra Study from invalid {name}");
    }
}
