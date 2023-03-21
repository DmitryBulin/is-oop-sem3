using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Xunit;

namespace Isu.Test;

public class GroupTest
{
    [Fact]
    public void AddStudentToGroupTwice_ThrowException()
    {
        var idGenerator = new StudentIdGenerator();
        var group = new Group(new GroupName(TestingConstants.GroupName));
        var student = new Student(group, TestingConstants.StudentName, idGenerator.NewId());
        Assert.Throws<StudentDuplicationException>(() => group.AddStudent(student));
    }

    [Fact]
    public void RemoveNonExistentStudent_ThrowException()
    {
        var idGenerator = new StudentIdGenerator();
        var firstGroup = new Group(new GroupName(TestingConstants.GroupName));
        var secondGroup = new Group(new GroupName(TestingConstants.GroupName));
        var student = new Student(firstGroup, TestingConstants.StudentName, idGenerator.NewId());
        Assert.Throws<StudentAbsenceException>(() => secondGroup.RemoveStudent(student));
    }

    [Fact]
    public void GetStudentInGroupHeIsNotIn_ThrowException()
    {
        var group = new Group(new GroupName(TestingConstants.GroupName));
        Assert.Throws<StudentAbsenceException>(() => group.GetStudent(0));
    }

    [Fact]
    public void FindStudentInGroup_ReturnStudentOrNull()
    {
        var idGenerator = new StudentIdGenerator();
        var group = new Group(new GroupName(TestingConstants.GroupName));
        var student = new Student(group, TestingConstants.StudentName, idGenerator.NewId());

        Assert.Equal(student, group.FindStudent(student.Id));
        Assert.Null(group.FindStudent(student.Id + 1));
    }

    [Fact]
    public void ChangeGroupCapacity_CapacityChanged()
    {
        var firstGroup = new Group(new GroupName(TestingConstants.GroupName));
        var secondGroup = new Group(new GroupName(TestingConstants.GroupName));
        var thirdGroup = new Group(new GroupName(TestingConstants.GroupName));
        int firstCapacity = 1;
        int secondCapacity = 50;
        int thirdCapacity = int.MaxValue;

        firstGroup.ChangeGroupCapacity(firstCapacity);
        secondGroup.ChangeGroupCapacity(secondCapacity);
        thirdGroup.ChangeGroupCapacity(thirdCapacity);

        Assert.Equal(firstCapacity, firstGroup.GroupCapacity);
        Assert.Equal(secondCapacity, secondGroup.GroupCapacity);
        Assert.Equal(thirdCapacity, thirdGroup.GroupCapacity);
    }

    [Fact]
    public void ChangeGroupCapacity_ThrowException()
    {
        var idGenerator = new StudentIdGenerator();
        var group = new Group(new GroupName(TestingConstants.GroupName));
        new Student(group, TestingConstants.StudentName, idGenerator.NewId());
        new Student(group, TestingConstants.StudentName, idGenerator.NewId());
        Assert.Throws<GroupCapacityException>(() => group.ChangeGroupCapacity(-100));
        Assert.Throws<GroupCapacityException>(() => group.ChangeGroupCapacity(0));
        Assert.Throws<GroupCapacityException>(() => group.ChangeGroupCapacity(1));
    }

    [Fact]
    public void CreateGroup_HaveCorrespondingToNameCourseNumber()
    {
        Assert.Equal(new CourseNumber(7), new Group(new GroupName("7111")).Name.CourseNumber);
        Assert.Equal(new CourseNumber(7), new Group(new GroupName("7112")).Name.CourseNumber);
        Assert.Equal(new CourseNumber(1), new Group(new GroupName("M11112")).Name.CourseNumber);
        Assert.Equal(new CourseNumber(2), new Group(new GroupName("M1234")).Name.CourseNumber);
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<InvalidGroupNameException>(() => new Group(new GroupName(string.Empty)));
        Assert.Throws<InvalidGroupNameException>(() => new Group(new GroupName("333")));
        Assert.Throws<InvalidGroupNameException>(() => new Group(new GroupName("MM303")));
        Assert.Throws<InvalidGroupNameException>(() => new Group(new GroupName("M3002")));
        Assert.Throws<InvalidGroupNameException>(() => new Group(new GroupName("0002")));
        Assert.Throws<InvalidGroupNameException>(() => new Group(new GroupName("M 3202")));
    }
}
