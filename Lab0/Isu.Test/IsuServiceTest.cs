using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var service = new IsuService();
        Group group = service.AddGroup(new GroupName(TestingConstants.GroupName));
        Student student = service.AddStudent(group, TestingConstants.StudentName);
        Assert.Equal(student.Group, group);
        Assert.Equal(group.GetStudent(student.Id), student);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var service = new IsuService();
        Group group = service.AddGroup(new GroupName(TestingConstants.GroupName));
        group.ChangeGroupCapacity(1);
        service.AddStudent(group, TestingConstants.StudentName);
        Assert.Throws<GroupCapacityException>(() => service.AddStudent(group, TestingConstants.StudentName));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var service = new IsuService();
        Group firstGroup = service.AddGroup(new GroupName("M1111"));
        Group secondGroup = service.AddGroup(new GroupName("M2222"));
        Student student = service.AddStudent(firstGroup, TestingConstants.StudentName);
        service.ChangeStudentGroup(student, secondGroup);
        Assert.Equal(student.Group, secondGroup);
        Assert.NotNull(secondGroup.FindStudent(student.Id));
        Assert.Null(firstGroup.FindStudent(student.Id));
    }

    [Fact]
    public void TransferStudentToSameGroup_ThrowException()
    {
        var service = new IsuService();
        Group group = service.AddGroup(new GroupName(TestingConstants.GroupName));
        Student student = service.AddStudent(group, TestingConstants.StudentName);
        Assert.Throws<GroupDuplicationException>(() => service.ChangeStudentGroup(student, group));
    }

    [Fact]
    public void CreateGroup_ReturnSameGroup()
    {
        var service = new IsuService();
        Group group = service.AddGroup(new GroupName(TestingConstants.GroupName));
        Assert.Equal(group, service.FindGroup(new GroupName(TestingConstants.GroupName)));
    }

    [Fact]
    public void FindGroupByName_ReturnGroupOrNull()
    {
        var service = new IsuService();
        Group group = service.AddGroup(new GroupName(TestingConstants.GroupName));
        Assert.Equal(group, service.FindGroup(new GroupName(TestingConstants.GroupName)));
        Assert.Null(service.FindGroup(new GroupName("7111")));
    }

    [Fact]
    public void FindGroupsByCourseName_ReturnAllCorrespondingGroupsOrEmptyList()
    {
        var service = new IsuService();
        var groups = new List<Group>();
        groups.Add(service.AddGroup(new GroupName("M3111"))); // Course 1.
        groups.Add(service.AddGroup(new GroupName("M3112"))); // Course 1.
        service.AddGroup(new GroupName("M3421")); // Course 4.
        Assert.Equal(groups, service.FindGroups(new CourseNumber(1)));
        Assert.Equal(new List<Group>(), service.FindGroups(new CourseNumber(2)));
    }

    [Fact]
    public void GetExistentStudentById_ReturnStudent()
    {
        var service = new IsuService();
        Group group = service.AddGroup(new GroupName(TestingConstants.GroupName));
        Student student = service.AddStudent(group, TestingConstants.StudentName);
        Assert.Equal(student, service.GetStudent(student.Id));
    }

    [Fact]
    public void GetNonExistentStudentById_ThrowException()
    {
        var service = new IsuService();
        Assert.Throws<StudentAbsenceException>(() => service.GetStudent(0));
    }

    [Fact]
    public void FindStudentById_ReturnsStudentOrNull()
    {
        var service = new IsuService();
        Group group = service.AddGroup(new GroupName(TestingConstants.GroupName));
        Student student = service.AddStudent(group, TestingConstants.StudentName);
        Assert.Equal(student, service.FindStudent(student.Id));
        Assert.Null(service.FindStudent(student.Id + 1));
    }

    [Fact]
    public void FindStudentsByGroupName_ReturnStudentsFromCorrespondingGroupOrEmptyList()
    {
        var service = new IsuService();
        Group firstGroup = service.AddGroup(new GroupName(TestingConstants.GroupName));
        Group secondGroup = service.AddGroup(new GroupName("M1111"));
        var students = new List<Student>();
        students.Add(service.AddStudent(firstGroup, TestingConstants.StudentName));
        students.Add(service.AddStudent(firstGroup, TestingConstants.StudentName));
        students.Add(service.AddStudent(firstGroup, TestingConstants.StudentName));
        service.AddStudent(secondGroup, TestingConstants.StudentName);
        Assert.Equal(students, service.FindStudents(new GroupName(TestingConstants.GroupName)));
        Assert.Equal(new List<Student>(), service.FindStudents(new GroupName("M2222")));
    }

    [Fact]
    public void FindStudentsInGroupsByCourseName_ReturnStudentFromAllCorrespondingGroupsOrEmptyList()
    {
        var service = new IsuService();
        Group firstGroup = service.AddGroup(new GroupName("M1111")); // Course 1.
        Group secondGroup = service.AddGroup(new GroupName("M1112")); // Course 1.
        Group thirdGroup = service.AddGroup(new GroupName("M1211")); // Course 2.
        var students = new List<Student>();
        students.Add(service.AddStudent(firstGroup, TestingConstants.StudentName));
        students.Add(service.AddStudent(firstGroup, TestingConstants.StudentName));
        students.Add(service.AddStudent(firstGroup, TestingConstants.StudentName));
        students.Add(service.AddStudent(secondGroup, TestingConstants.StudentName));
        students.Add(service.AddStudent(secondGroup, TestingConstants.StudentName));
        service.AddStudent(thirdGroup, TestingConstants.StudentName);
        Assert.Equal(students, service.FindStudents(new CourseNumber(1)));
        Assert.Equal(new List<Student>(), service.FindStudents(new CourseNumber(3)));
    }
}