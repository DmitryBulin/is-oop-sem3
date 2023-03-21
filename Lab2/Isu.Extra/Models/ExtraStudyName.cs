using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public record ExtraStudyName
{
    public ExtraStudyName(string name)
    {
        if (!IsValidName(name))
        {
            throw ExtraStudyException.InvalidName(name);
        }

        Name = name;
    }

    public string Name { get; }

    public static bool IsValidName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return false;
        }

        return true;
    }

    public override string ToString() => Name;
}
