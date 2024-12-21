using Itmo.ObjectOrientedProgramming.Lab2.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.EvaluationFormat;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Xunit;

namespace Lab2.Tests;

public class SubjectTests
{
    private readonly User _author = new("Victor");
    private readonly User _anotherAuthor = new("Another Victor");
    private readonly UserDirector _userDirector;

    private static List<LectureMaterial> LectureMaterials => new List<LectureMaterial>();

    public SubjectTests()
    {
        _userDirector = new UserDirector(_author);
    }

    [Fact]
    public void SubjectCreationWithInvalidTotalPoints()
    {
        // Arrange
        var laboratories = new List<Laboratory>
        {
            CreateLaboratory("Lab1", 30),
            CreateLaboratory("Lab2", 30),
        };
        var evaluationFormat = new Exam(30);

        // Act
        ResultType result = _userDirector.CreateSubject("Subject1", laboratories, LectureMaterials, evaluationFormat);

        // Assert
        Assert.IsType<ErrorTotalPointsNotEqual100>(result);
    }

    [Fact]
    public void SubjectCreationWithValidTotalPoints()
    {
        // Arrange
        var laboratories = new List<Laboratory>
        {
            CreateLaboratory("Lab1", 50),
        };
        var evaluationFormat = new Exam(50);

        // Act
        Subject subject = CreateSubject("Subject1", laboratories, evaluationFormat);

        // Assert
        Assert.Equal(100, subject.LaboratoryList.Sum(lab => lab.Points) + subject.EvaluationFormat.Points);
    }

    [Fact]
    public void SubjectCloneShouldContainBaseSubjectId()
    {
        // Arrange
        var laboratories = new List<Laboratory>
        {
            CreateLaboratory("Lab1", 50),
        };
        var evaluationFormat = new Exam(50);

        Subject subject = CreateSubject("Subject1", laboratories, evaluationFormat);

        // Act
        Subject clonedSubject = subject.Clone(_anotherAuthor);

        // Assert
        Assert.Equal(subject.SubjectId, clonedSubject.BaseSubjectId);
        Assert.NotEqual(subject.SubjectId, clonedSubject.SubjectId);
    }

    [Fact]
    public void SubjectCannotBeChangeNotByAuthor()
    {
        // Arrange
        var laboratories = new List<Laboratory>
        {
            CreateLaboratory("Lab1", 50),
        };
        var evaluationFormat = new Exam(50);

        Subject subject = CreateSubject("Subject1", laboratories, evaluationFormat);

        // Act
        ResultType changeResult = subject.TryChangeSubject("Subject1 smth new", LectureMaterials, _anotherAuthor);

        // Assert
        Assert.IsType<ErrorUnauthorizedAccess>(changeResult);
    }

    [Fact]
    public void SubjectCanBeChangeByAuthor()
    {
        // Arrange
        var laboratories = new List<Laboratory>
        {
            CreateLaboratory("Lab1", 50),
        };
        var evaluationFormat = new Exam(50);

        Subject subject = CreateSubject("Subject1", laboratories, evaluationFormat);

        // Act
        ResultType changeResult = subject.TryChangeSubject("Subject1 smth new", LectureMaterials, _author);

        // Assert
        Assert.IsType<SuccessResult>(changeResult);
        Assert.Equal("Subject1 smth new", subject.Name);
    }

    [Fact]
    public void SubjectShouldAddToRepository()
    {
        // Arrange
        var laboratories = new List<Laboratory>
        {
            CreateLaboratory("Lab1", 40),
            CreateLaboratory("Lab2", 40),
        };
        var evaluationFormat = new Exam(20);

        // Act
        Subject subject = CreateSubject("Subject1", laboratories, evaluationFormat);

        // Assert
        Assert.Equal(SubjectRepository.Instance.GetById(subject.SubjectId), subject);
    }

    private Laboratory CreateLaboratory(string title, int points)
    {
        ResultType result = _userDirector.CreateLaboratory(title, "Description", "Criteria", points);
        return Assert.IsType<SuccessResultWrapper<Laboratory>>(result).Value;
    }

    private Subject CreateSubject(string name, List<Laboratory> laboratories, IEvaluationFormat evaluationFormat)
    {
        ResultType result = _userDirector.CreateSubject(name, laboratories, LectureMaterials, evaluationFormat);
        return Assert.IsType<SuccessResultWrapper<Subject>>(result).Value;
    }
}