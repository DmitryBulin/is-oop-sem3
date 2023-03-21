using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Services;

public interface IExtraStudyService
{
    IReadOnlyList<ExtraStudyGroup> ExtraStudyGroups { get; }
    IReadOnlyList<ExtraStudy> ExtraStudies { get; }

    ExtraStudy AddExtraStudy(Faculty faculty, ExtraStudyName name);
    ExtraStudyGroup AddGroup(GroupName name, ExtraStudy extraStudy);
    void AddStudentToGroup(Student student, ExtraStudyGroup group);
    void ChangeSchedule(ISchedule schedule);
    List<Student> FindStudentsWithoutExtraStudy(Group group);
    List<ExtraStudyGroup> GetGroupsInExtraStudy(ExtraStudy extraStudy);
    List<Student> GetStudents(ExtraStudy extraStudy);
    List<Student> GetStudents(ExtraStudyGroup group);
    void RemoveExtraStudy(ExtraStudy extraStudy);
    void RemoveGroup(ExtraStudyGroup group);
    void RemoveStudentFromGroup(Student student, ExtraStudyGroup group);
}