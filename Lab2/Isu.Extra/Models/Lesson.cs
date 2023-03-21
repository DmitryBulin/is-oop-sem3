using Isu.Entities;

namespace Isu.Extra.Models;

public record Lesson
{
    public Lesson(DateTime time, Group group)
    {
        Time = time;
        Group = group;
    }

    public DateTime Time { get; }
    public Group Group { get; }
}
