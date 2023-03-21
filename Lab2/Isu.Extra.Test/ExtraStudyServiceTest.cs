using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Xunit;

namespace Isu.Extra.Test;

public class ExtraStudyServiceTest
{
    [Fact]
    public void AddNewExtraStudy_ExtraStudyAdded()
    {
        IExtraStudyService service = TestingConstants.DefaultService;
        ExtraStudyName name = TestingConstants.DefaultExtraStudyName;
        Faculty faculty = TestingConstants.DefaultFaculty;

        ExtraStudy extraStudy = service.AddExtraStudy(faculty, name);

        Assert.NotNull(extraStudy);
        Assert.Contains(extraStudy, service.ExtraStudies);
        Assert.Contains(extraStudy, faculty.ExtraStudies);
    }

    [Fact]
    public void GetExtraStudyGroups_AllGroupsGot()
    {
        IExtraStudyService service = TestingConstants.DefaultService;
        ExtraStudyName name = TestingConstants.DefaultExtraStudyName;
        Faculty faculty = TestingConstants.DefaultFaculty;
        List<GroupName> groupNames = TestingConstants.GetGroupNames(5);
        var groups = new List<ExtraStudyGroup>();

        ExtraStudy extraStudy = service.AddExtraStudy(faculty, name);
        groupNames.ForEach(name => groups.Add(service.AddGroup(name, extraStudy)));
        List<ExtraStudyGroup> requestedGroups = service.GetGroupsInExtraStudy(extraStudy);

        groups.ForEach(group => Assert.Contains(group, requestedGroups));
    }

    [Fact]
    public void AddStudentToExtraStudyGroup_StudentAdded()
    {
        IExtraStudyService service = TestingConstants.DefaultService;
        ExtraStudyName name = TestingConstants.DefaultExtraStudyName;
        Faculty faculty = TestingConstants.DefaultFaculty;
        Student student = TestingConstants.AddStudents(TestingConstants.DefaultGroup, 1)[0];

        ExtraStudy extraStudy = service.AddExtraStudy(faculty, name);
        ExtraStudyGroup extraStudyGroup = service.AddGroup(TestingConstants.GetGroupNames(1)[0], extraStudy);
        service.AddStudentToGroup(student, extraStudyGroup);

        Assert.Contains(student, extraStudyGroup.Students);
    }

    [Fact]
    public void AddStudentToExtraStudyWithSameFaculy_ExceptionThrowed()
    {
        IExtraStudyService service = TestingConstants.DefaultService;
        ExtraStudyName name = TestingConstants.DefaultExtraStudyName;
        Faculty faculty = TestingConstants.DefaultFaculty;
        Student student = TestingConstants.AddStudents(TestingConstants.DefaultGroup, 1)[0];

        faculty.AddGroupName(student.Group.Name);
        ExtraStudy extraStudy = service.AddExtraStudy(faculty, name);
        ExtraStudyGroup extraStudyGroup = service.AddGroup(TestingConstants.GetGroupNames(1)[0], extraStudy);

        Assert.Throws<ExtraStudyServiceException>(() => service.AddStudentToGroup(student, extraStudyGroup));
    }

    [Fact]
    public void AddStudentWithIntersectingSchedule_ExceptionThrowed()
    {
        IExtraStudyService service = TestingConstants.DefaultService;
        ExtraStudyName name = TestingConstants.DefaultExtraStudyName;
        Faculty faculty = TestingConstants.DefaultFaculty;
        Student student = TestingConstants.AddStudents(TestingConstants.DefaultGroup, 1)[0];

        ExtraStudy extraStudy = service.AddExtraStudy(faculty, name);
        ExtraStudyGroup extraStudyGroup = service.AddGroup(TestingConstants.GetGroupNames(1)[0], extraStudy);
        service.ChangeSchedule(Schedule.Builder
            .WithLesson(new Lesson(DateTime.MinValue, student.Group))
            .WithLesson(new Lesson(DateTime.MinValue, extraStudyGroup))
            .Build());

        Assert.Throws<ExtraStudyServiceException>(() => service.AddStudentToGroup(student, extraStudyGroup));
    }

    [Fact]
    public void AddStudentToMoreThanTwoExtraStudies_ExceptionThrowed()
    {
        IExtraStudyService service = TestingConstants.DefaultService;
        ExtraStudyName name = TestingConstants.DefaultExtraStudyName;
        Faculty faculty = TestingConstants.DefaultFaculty;
        Student student = TestingConstants.AddStudents(TestingConstants.DefaultGroup, 1)[0];

        ExtraStudy extraStudy = service.AddExtraStudy(faculty, name);
        ExtraStudyGroup extraStudyGroup1 = service.AddGroup(TestingConstants.GetGroupNames(1)[0], extraStudy);
        ExtraStudyGroup extraStudyGroup2 = service.AddGroup(TestingConstants.GetGroupNames(2)[1], extraStudy);
        ExtraStudyGroup extraStudyGroup3 = service.AddGroup(TestingConstants.GetGroupNames(3)[2], extraStudy);

        service.AddStudentToGroup(student, extraStudyGroup1);
        service.AddStudentToGroup(student, extraStudyGroup2);

        Assert.Throws<ExtraStudyServiceException>(() => service.AddStudentToGroup(student, extraStudyGroup3));
    }

    [Fact]
    public void RemoveStudentFromExtraStudyGroup_StudentRemoved()
    {
        IExtraStudyService service = TestingConstants.DefaultService;
        ExtraStudyName name = TestingConstants.DefaultExtraStudyName;
        Faculty faculty = TestingConstants.DefaultFaculty;
        Student student = TestingConstants.AddStudents(TestingConstants.DefaultGroup, 1)[0];

        ExtraStudy extraStudy = service.AddExtraStudy(faculty, name);
        ExtraStudyGroup extraStudyGroup = service.AddGroup(TestingConstants.GetGroupNames(1)[0], extraStudy);
        service.AddStudentToGroup(student, extraStudyGroup);
        service.RemoveStudentFromGroup(student, extraStudyGroup);

        Assert.DoesNotContain(student, extraStudyGroup.Students);
    }

    [Fact]
    public void FindStudentsWithoutExtraStudies_AllStudentsFound()
    {
        IExtraStudyService service = TestingConstants.DefaultService;
        ExtraStudyName name = TestingConstants.DefaultExtraStudyName;
        Faculty faculty = TestingConstants.DefaultFaculty;
        Group group = TestingConstants.DefaultGroup;
        List<Student> students = TestingConstants.AddStudents(group, 5);
        var studentsWithoutExtraStudy = new List<Student> { students[3], students[4] };

        ExtraStudy extraStudy = service.AddExtraStudy(faculty, name);
        ExtraStudyGroup extraStudyGroup = service.AddGroup(TestingConstants.GetGroupNames(1)[0], extraStudy);
        service.AddStudentToGroup(students[0], extraStudyGroup);
        service.AddStudentToGroup(students[1], extraStudyGroup);
        service.AddStudentToGroup(students[2], extraStudyGroup);
        List<Student> requestedStudents = service.FindStudentsWithoutExtraStudy(group);

        studentsWithoutExtraStudy.ForEach(student => Assert.Contains(student, requestedStudents));
    }

    [Fact]
    public void FindStudentsInExtraStudyGroup_AllStudentsGot()
    {
        IExtraStudyService service = TestingConstants.DefaultService;
        ExtraStudyName name = TestingConstants.DefaultExtraStudyName;
        Faculty faculty = TestingConstants.DefaultFaculty;
        Group group = TestingConstants.DefaultGroup;
        List<Student> students = TestingConstants.AddStudents(group, 5);

        ExtraStudy extraStudy = service.AddExtraStudy(faculty, name);
        ExtraStudyGroup extraStudyGroup = service.AddGroup(TestingConstants.GetGroupNames(1)[0], extraStudy);
        students.ForEach(student => service.AddStudentToGroup(student, extraStudyGroup));
        List<Student> requestedStudents = service.GetStudents(extraStudyGroup);

        students.ForEach(student => Assert.Contains(student, requestedStudents));
    }
}
