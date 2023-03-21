using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public interface ISchedule
{
    IReadOnlyList<Lesson> Lessons { get; }

    bool GroupsLessonsIntersect(Group firstGroup, Group secondGroup);
}