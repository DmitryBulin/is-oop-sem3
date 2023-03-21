using Isu.Entities;
using Isu.Models;

namespace Isu.Extra.Entities;

public class ExtraStudyGroup : Group
{
    public ExtraStudyGroup(GroupName name, ExtraStudy extraStudy)
        : base(name)
    {
        ExtraStudy = extraStudy;
    }

    public ExtraStudy ExtraStudy { get; }
}
