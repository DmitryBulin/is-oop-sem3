using Isu.Exceptions;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class ExtraStudy
{
    private readonly List<ExtraStudyGroup> _groups = new List<ExtraStudyGroup>();

    public ExtraStudy(Faculty faculty, ExtraStudyName name)
    {
        Faculty = faculty;
        Name = name;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public ExtraStudyName Name { get; }
    public Faculty Faculty { get; }
    public IReadOnlyList<ExtraStudyGroup> Groups => _groups;

    public ExtraStudyGroup AddGroup(GroupName name)
    {
        if (_groups.Any(group => group.Name == name))
        {
            throw new GroupDuplicationException(name);
        }

        var group = new ExtraStudyGroup(name, this);
        _groups.Add(group);
        return group;
    }

    public void RemoveGroup(ExtraStudyGroup group)
    {
        if (!_groups.Remove(group))
        {
            throw ExtraStudyException.GroupAbcentException(Name, group.Name);
        }
    }
}
