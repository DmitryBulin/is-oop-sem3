using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;
public class IsuService : IIsuService
{
    private List<Group> _groups = new List<Group>();
    private StudentIdGenerator _studentIdGenerator = new StudentIdGenerator();

    public Group AddGroup(GroupName name)
    {
        if (_groups.Find(group => group.Name == name) is not null)
        {
            throw new GroupDuplicationException(name);
        }

        var newGroup = new Group(name);
        _groups.Add(newGroup);
        return newGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        var newStudent = new Student(group, name, _studentIdGenerator.NewId());
        return newStudent;
    }

    public Student GetStudent(int id)
    {
        return FindStudent(id) ?? throw new StudentAbsenceException(id);
    }

    public Student? FindStudent(int id)
    {
        return _groups.Select(group => group.FindStudent(id)).FirstOrDefault();
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        Group? group = _groups.Find(group => group.Name == groupName);

        return group?.Students.ToList() ?? new List<Student>();
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return _groups
            .FindAll(group => group.Name.CourseNumber == courseNumber)
            .SelectMany(group => group.Students)
            .ToList();
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.Find(group => group.Name == groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groups.Where(group => group.Name.CourseNumber == courseNumber).ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (student.Group.Equals(newGroup))
        {
            throw new GroupDuplicationException(newGroup.Name);
        }

        student.MoveToAnotherGroup(newGroup);
    }
}