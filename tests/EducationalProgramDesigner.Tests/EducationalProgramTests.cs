using Itmo.ObjectOrientedProgramming.Lab2.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.EvaluationFormat;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Xunit;

namespace Lab2.Tests;

public class EducationalProgramTests
{
    private readonly User _author = new("Victor");
    private readonly User _responsiblePerson = new("Responsible Person");
    private readonly User _programManager = new("Program Manager");
    private readonly UserDirector _userDirector;

    private static List<LectureMaterial> LectureMaterials => new List<LectureMaterial>();

    public EducationalProgramTests()
    {
        _userDirector = new UserDirector(_author);
    }

    [Fact]
    public void EducationalProgramCreation()
    {
        // Arrange
        var laboratories = new List<Laboratory>
        {
            CreateLaboratory("Lab1", 40),
            CreateLaboratory("Lab2", 40),
        };
        var evaluationFormat = new Exam(20);

        Subject subject = CreateSubject("Subject1", laboratories, evaluationFormat);
        Subject subject2 = CreateSubject("Subject2", laboratories, evaluationFormat);

        var semesters = new List<Semester>
        {
            new Semester(1),
            new Semester(2),
        };
        semesters[0].AddSubject(subject);
        semesters[1].AddSubject(subject);
        semesters[1].AddSubject(subject2);

        // Act
        EducationalProgram program = CreateEducationalProgram("Program1", semesters);

        // Assert
        Assert.Equal(2, program.Semesters.Count);
        Assert.Contains(program.Semesters[0].Subjects, s => s == subject);
    }

    [Fact]
    public void EducationalProgramShouldAddToRepository()
    {
        // Arrange
        var semesters = new List<Semester>
        {
            new Semester(1),
            new Semester(2),
        };

        // Act
        EducationalProgram program = CreateEducationalProgram("Program1", semesters);

        // Assert
        Assert.Equal(EducationalProgramRepository.Instance.GetById(program.EducationalProgramId), program);
    }

    [Fact]
    public void EducationalProgramAddSubject()
    {
        // Arrange
        var laboratories = new List<Laboratory> { CreateLaboratory("Lab1", 80), };
        var evaluationFormat = new Exam(20);

        Subject subject = CreateSubject("Subject1", laboratories, evaluationFormat);

        var semesters = new List<Semester>
        {
            new Semester(1),
            new Semester(2),
        };

        EducationalProgram program = CreateEducationalProgram("Program1", semesters);

        // Act
        program.AddSubjectToSemester(subject, 1);

        // Assert
        Assert.Contains(program.Semesters[0].Subjects, s => s == subject);
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

    private EducationalProgram CreateEducationalProgram(string title, IList<Semester> semesters)
    {
        ResultType result = _userDirector.CreateEducationalProgram(title, _responsiblePerson, _programManager, semesters);
        return Assert.IsType<SuccessResultWrapper<EducationalProgram>>(result).Value;
    }
}