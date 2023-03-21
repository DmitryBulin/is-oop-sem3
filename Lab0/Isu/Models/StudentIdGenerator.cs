using Isu.Exceptions;

namespace Isu.Models;

public class StudentIdGenerator
{
    private const int MaxId = 999999;

    private int _currentId = 100000;

    public int NewId()
    {
        if (_currentId > MaxId)
        {
            throw new InvalidStudentIdException();
        }

        return _currentId++;
    }
}
