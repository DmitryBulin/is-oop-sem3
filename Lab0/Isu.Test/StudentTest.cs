using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Xunit;

namespace Isu.Test;

public class StudentTest
{
    [Fact]
    public void CreateStudentWithInvalidName_ThrowException()
    {
        var idGenerator = new StudentIdGenerator();
        var group = new Group(new GroupName(TestingConstants.GroupName));
        Assert.Throws<InvalidStudentNameException>(() => new Student(group, string.Empty, idGenerator.NewId()));
        Assert.Throws<InvalidStudentNameException>(() => new Student(group, "test Name", idGenerator.NewId()));
        Assert.Throws<InvalidStudentNameException>(() => new Student(group, "Test name", idGenerator.NewId()));
        Assert.Throws<InvalidStudentNameException>(() => new Student(group, "TestName", idGenerator.NewId()));
        Assert.Throws<InvalidStudentNameException>(() => new Student(group, "Te Na", idGenerator.NewId()));
        Assert.Throws<InvalidStudentNameException>(() => new Student(group, "Tes1 N2me", idGenerator.NewId()));
        Assert.Throws<InvalidStudentNameException>(() => new Student(group, "Test! Name", idGenerator.NewId()));
        Assert.Throws<InvalidStudentNameException>(() => new Student(group, "Test  Name", idGenerator.NewId()));
    }

    [Fact]
    public void ChangeStudentNameToValidName_NameChanged()
    {
        var idGenerator = new StudentIdGenerator();
        var group = new Group(new GroupName(TestingConstants.GroupName));
        var student = new Student(group, TestingConstants.StudentName, idGenerator.NewId());
        student.ChangeName("Name Test");
        Assert.Equal("Name Test", student.Name);
    }

    [Fact]
    public void ChangeStudentNameToInvalidName_ThrowException()
    {
        var idGenerator = new StudentIdGenerator();
        var group = new Group(new GroupName(TestingConstants.GroupName));
        var student = new Student(group, TestingConstants.StudentName, idGenerator.NewId());
        Assert.Throws<InvalidStudentNameException>(() => student.ChangeName(string.Empty));
    }
}
