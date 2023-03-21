using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private List<Student> _students = new List<Student>();
    public Group(GroupName name)
    {
        Name = name;
    }

    public IReadOnlyList<Student> Students => _students;
    public GroupName Name { get; }
    public int GroupCapacity { get; private set; } = 20;

    public void ChangeGroupCapacity(int newCapacity)
    {
        if (newCapacity <= 0)
        {
            throw new GroupCapacityException(Name, GroupCapacity, newCapacity);
        }

        if (newCapacity < _students.Count)
        {
            throw new GroupCapacityException(newCapacity, _students.Count, Name);
        }

        GroupCapacity = newCapacity;
    }

    public Student? FindStudent(int id)
    {
        return _students.Find(s => s.Id == id);
    }

    public Student GetStudent(int id)
    {
        return FindStudent(id) ?? throw new StudentAbsenceException(id);
    }

    public void AddStudent(Student student)
    {
        if (_students.Contains(student))
        {
            throw new StudentDuplicationException(student.Id, Name);
        }

        if (_students.Count == GroupCapacity)
        {
            throw new GroupCapacityException(Name, student);
        }

        _students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        if (!_students.Remove(student))
        {
            throw new StudentAbsenceException(student.Id);
        }
    }
}
