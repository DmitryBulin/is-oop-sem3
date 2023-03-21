using System.Text.RegularExpressions;
using Isu.Exceptions;

namespace Isu.Entities;
public class Student
{
    private static readonly Regex[] StudentNamePatterns = { new Regex(@"^[A-Z][a-z]{2,29}\s[A-Z][a-z]{2,29}$", RegexOptions.Compiled) };

    private string _name = string.Empty;

    public Student(Group group, string name, int id)
    {
        Id = id;
        Group = group;
        ChangeName(name);
        Group.AddStudent(this);
    }

    public Group Group { get; private set; }
    public string Name { get => _name; set => ChangeName(value); }
    public int Id { get; }

    public static bool IsValidName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return false;
        }

        if (StudentNamePatterns.Any(pattern => pattern.IsMatch(name)))
        {
            return true;
        }

        return false;
    }

    public void ChangeName(string name)
    {
        if (IsValidName(name))
        {
            _name = name;
        }
        else
        {
            throw new InvalidStudentNameException(name, Id);
        }
    }

    public void MoveToAnotherGroup(Group group)
    {
        group.AddStudent(this);
        Group.RemoveStudent(this);
        Group = group;
    }
}