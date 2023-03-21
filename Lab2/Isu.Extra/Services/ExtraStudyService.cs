using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Services;

public class ExtraStudyService : IExtraStudyService
{
    private const int MaxExtraStudiesPerStudent = 2;

    private readonly List<ExtraStudy> _extraStudies = new List<ExtraStudy>();
    private readonly List<ExtraStudyGroup> _extraStudyGroups = new List<ExtraStudyGroup>();
    private ISchedule _schedule;

    public ExtraStudyService(ISchedule schedule)
    {
        _schedule = schedule;
    }

    public IReadOnlyList<ExtraStudy> ExtraStudies => _extraStudies;
    public IReadOnlyList<ExtraStudyGroup> ExtraStudyGroups => _extraStudyGroups;

    public ExtraStudy AddExtraStudy(Faculty faculty, ExtraStudyName name)
    {
        ExtraStudy extraStudy = faculty.AddExtraStudy(name);
        _extraStudies.Add(extraStudy);
        return extraStudy;
    }

    public void RemoveExtraStudy(ExtraStudy extraStudy)
    {
        extraStudy.Faculty.RemoveExtraStudy(extraStudy);
        _extraStudies.Remove(extraStudy);
    }

    public ExtraStudyGroup AddGroup(GroupName name, ExtraStudy extraStudy)
    {
        ExtraStudyGroup group = extraStudy.AddGroup(name);
        _extraStudyGroups.Add(group);
        return group;
    }

    public void RemoveGroup(ExtraStudyGroup group)
    {
        group.ExtraStudy.RemoveGroup(group);
        _extraStudyGroups.Remove(group);
    }

    public void AddStudentToGroup(Student student, ExtraStudyGroup group)
    {
        if (group.ExtraStudy.Faculty.GroupNames.Contains(student.Group.Name))
        {
            throw ExtraStudyServiceException.StudentFromSameFacultyException(student.Id, group.Name);
        }

        if (_extraStudyGroups.Where(group => group.Students.Contains(student)).Count() >= MaxExtraStudiesPerStudent)
        {
            throw ExtraStudyServiceException.StudentMaxExtraStudyExceededException(student.Id);
        }

        if (_schedule.GroupsLessonsIntersect(student.Group, group))
        {
            throw ExtraStudyServiceException.ExtraStudyGroupLessonsIntersects(student.Id, group.Name);
        }

        group.AddStudent(student);
    }

    public void RemoveStudentFromGroup(Student student, ExtraStudyGroup group)
    {
        group.RemoveStudent(student);
    }

    public List<ExtraStudyGroup> GetGroupsInExtraStudy(ExtraStudy extraStudy)
    {
        return extraStudy.Groups.ToList();
    }

    public List<Student> GetStudents(ExtraStudy extraStudy)
    {
        return extraStudy.Groups.SelectMany(group => group.Students).ToList();
    }

    public List<Student> GetStudents(ExtraStudyGroup group)
    {
        return group.Students.ToList();
    }

    public List<Student> FindStudentsWithoutExtraStudy(Group group)
    {
        return group.Students
            .Where(student => !_extraStudyGroups
                .SelectMany(extraStudyGroup => extraStudyGroup.Students)
                .Contains(student))
            .ToList();
    }

    public void ChangeSchedule(ISchedule schedule)
    {
        if (_extraStudyGroups.Any(group => group.Students.Any(student => schedule.GroupsLessonsIntersect(group, student.Group))))
        {
            throw ExtraStudyServiceException.NewScheduleIntersectLessons();
        }

        _schedule = schedule;
    }
}
