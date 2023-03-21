using Isu.Exceptions;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class Faculty
{
    private readonly List<ExtraStudy> _extraStudies = new List<ExtraStudy>();
    private readonly List<GroupName> _groupNames = new List<GroupName>();

    public Faculty(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public IReadOnlyList<ExtraStudy> ExtraStudies => _extraStudies;
    public IReadOnlyList<GroupName> GroupNames => _groupNames;

    public ExtraStudy AddExtraStudy(ExtraStudyName name)
    {
        if (_extraStudies.Any(extraStudy => extraStudy.Name == name))
        {
            throw FacultyException.ExtraStudyDuplicationException(name, Name);
        }

        var extraStudy = new ExtraStudy(this, name);
        _extraStudies.Add(extraStudy);
        return extraStudy;
    }

    public void RemoveExtraStudy(ExtraStudy extraStudy)
    {
        if (!_extraStudies.Remove(extraStudy))
        {
            throw FacultyException.ExtraStudyAbcentException(extraStudy.Name, Name);
        }
    }

    public void AddGroupName(GroupName name)
    {
        if (_groupNames.Contains(name))
        {
            throw new GroupDuplicationException(name);
        }

        _groupNames.Add(name);
    }

    public void RemoveGroupName(GroupName name)
    {
        if (!_groupNames.Remove(name))
        {
            throw FacultyException.GroupNameAbcentException(name, Name);
        }
    }
}
