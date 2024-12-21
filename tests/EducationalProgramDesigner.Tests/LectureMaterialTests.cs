using Itmo.ObjectOrientedProgramming.Lab2.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Xunit;

namespace Lab2.Tests;

public class LectureMaterialTests
{
    private readonly User _author = new("Victor");
    private readonly User _anotherAuthor = new("Another Victor");
    private readonly UserDirector _userDirector;

    public LectureMaterialTests()
    {
        _userDirector = new UserDirector(_author);
    }

    [Fact]
    public void LectureMaterialCanBeChangeByAuthor()
    {
        // Arrange
        LectureMaterial lecture = CreateLectureMaterial("Lecture1");

        // Act
        ResultType changeResult = lecture.TryChangeLectureMaterial("Lecture1 smth new", "Description smth new", "Content smth new", _author);

        // Assert
        Assert.IsType<SuccessResult>(changeResult);
        Assert.Equal("Lecture1 smth new", lecture.Title);
    }

    [Fact]
    public void LectureMaterialCannotBeChangeNotByAuthor()
    {
        // Arrange
        LectureMaterial lecture = CreateLectureMaterial("Lecture1");

        // Act
        ResultType changeResult = lecture.TryChangeLectureMaterial("Lecture1 smth new", "Description smth new", "Content smth new", _anotherAuthor);

        // Assert
        Assert.IsType<ErrorUnauthorizedAccess>(changeResult);
    }

    [Fact]
    public void LectureMaterialCloneShouldContainBaseLectureId()
    {
        // Arrange
        LectureMaterial lecture = CreateLectureMaterial("Lecture1");

        // Act
        LectureMaterial clonedLecture = lecture.Clone(_author);

        // Assert
        Assert.Equal(lecture.LectureId, clonedLecture.BaseLectureId);
        Assert.NotEqual(lecture.LectureId, clonedLecture.LectureId);
    }

    [Fact]
    public void LectureMaterialShouldAddToRepository()
    {
        // Act
        LectureMaterial lecture = CreateLectureMaterial("Lecture1");
        LectureMaterial lecture2 = CreateLectureMaterial("Lecture2");

        // Assert
        Assert.Equal(LectureMaterialRepository.Instance.GetById(lecture.LectureId), lecture);
        Assert.Equal(LectureMaterialRepository.Instance.GetById(lecture2.LectureId), lecture2);
    }

    private LectureMaterial CreateLectureMaterial(string title)
    {
        ResultType createResult = _userDirector.CreateLectureMaterial(title, "Description", "Content");
        return Assert.IsType<SuccessResultWrapper<LectureMaterial>>(createResult).Value;
    }
}