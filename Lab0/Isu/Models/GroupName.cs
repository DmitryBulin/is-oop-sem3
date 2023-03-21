using System.Globalization;
using System.Text.RegularExpressions;
using Isu.Exceptions;

namespace Isu.Models;

public record GroupName
{
    private static readonly Regex StandardGroupNamePattern = new Regex(@"^[A-Z]\d[1-5]\d{2,3}c?$", RegexOptions.Compiled);
    private const int StandardCourseNumberPosition = 2;

    private static readonly Regex PhdGroupNamePattern = new Regex(@"^[7-8]\d{3}$", RegexOptions.Compiled);
    private const int PhdCourseNumberPosition = 0;

    public GroupName(string name)
    {
        int courseNumberPosition;

        if (StandardGroupNamePattern.IsMatch(name))
        {
            Name = name;
            courseNumberPosition = StandardCourseNumberPosition;
        }
        else if (PhdGroupNamePattern.IsMatch(name))
        {
            Name = name;
            courseNumberPosition = PhdCourseNumberPosition;
        }
        else
        {
            throw new InvalidGroupNameException(name);
        }

        int courseNumber = CharUnicodeInfo.GetDigitValue(Name[courseNumberPosition]);
        CourseNumber = new CourseNumber(courseNumber);
    }

    public string Name { get; }
    public CourseNumber CourseNumber { get; }

    public static bool IsValidName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return false;
        }

        if (StandardGroupNamePattern.IsMatch(name) || PhdGroupNamePattern.IsMatch(name))
        {
            return true;
        }

        return false;
    }

    public override string ToString() => Name;
}
