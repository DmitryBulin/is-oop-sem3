using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;

namespace Isu.Extra.Test;

public static class TestingConstants
{
    public static ISchedule EmptySchedule => Schedule.Builder.Build();

    public static ExtraStudyName DefaultExtraStudyName => new ExtraStudyName("Default Extra Study name");

    public static Faculty DefaultFaculty => new Faculty("Default Faculty Name");

    public static IExtraStudyService DefaultService => new ExtraStudyService(EmptySchedule);

    public static Group DefaultGroup => new Group(GetGroupNames(1)[0]);

    public static List<GroupName> GetGroupNames(int count)
    {
        var names = new List<GroupName>();
        for (int i = 0; i < count; ++i)
        {
            names.Add(new GroupName("M320" + i));
        }

        return names;
    }

    public static List<Student> AddStudents(Group group, int count)
    {
        var students = new List<Student>();
        for (int i = 0; i < count; ++i)
        {
            var student = new Student(group, "Default Name", i);
            students.Add(student);
        }

        return students;
    }
}
