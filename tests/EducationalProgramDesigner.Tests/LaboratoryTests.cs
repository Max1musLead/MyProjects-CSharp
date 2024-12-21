using Itmo.ObjectOrientedProgramming.Lab2.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Xunit;

namespace Lab2.Tests;

public class LaboratoryTests
{
    private readonly User _author = new("Victor");
    private readonly User _anotherAuthor = new("Another Victor");
    private readonly UserDirector _userDirector;

    public LaboratoryTests()
    {
        _userDirector = new UserDirector(_author);
    }

    [Fact]
    public void LaboratoryCanBeChangeByAuthor()
    {
        // Arrange
        Laboratory laboratory = CreateLaboratory("Lab1", 50);

        // Act
        ResultType changeResult = laboratory.TryChangeLaboratory("Lab1 smth new", "Description smth new", "Criteria smth new", _author);

        // Assert
        Assert.IsType<SuccessResult>(changeResult);
        Assert.Equal("Lab1 smth new", laboratory.Title);
    }

    [Fact]
    public void LaboratoryCannotBeChangeNotByAuthor()
    {
        // Arrange
        Laboratory laboratory = CreateLaboratory("Lab1", 50);

        // Act
        ResultType changeResult = laboratory.TryChangeLaboratory("Lab1 smth new", "Description smth new", "Criteria smth new", _anotherAuthor);

        // Assert
        Assert.IsType<ErrorUnauthorizedAccess>(changeResult);
    }

    [Fact]
    public void LaboratoryCloneShouldContainBaseLaboratoryId()
    {
        // Arrange
        Laboratory laboratory = CreateLaboratory("Lab1", 50);

        // Act
        Laboratory clonedLab = laboratory.Clone(_author);
        Laboratory clonedLab2 = clonedLab.Clone(_anotherAuthor);

        // Assert
        Assert.Equal(laboratory.LaboratoryId, clonedLab.BaseLaboratoryId);
        Assert.NotEqual(laboratory.LaboratoryId, clonedLab.LaboratoryId);

        Assert.Equal(clonedLab.LaboratoryId, clonedLab2.BaseLaboratoryId);
        Assert.NotEqual(clonedLab.LaboratoryId, clonedLab2.LaboratoryId);
    }

    [Fact]
    public void LaboratoryShouldAddToRepository()
    {
        // Arrange
        var userDirector = new UserDirector(_author);

        // Act
        Laboratory laboratory = CreateLaboratory("Lab1", 50);
        Laboratory laboratory2 = CreateLaboratory("Lab2", 70);

        // Assert
        Assert.Equal(LaboratoryRepository.Instance.GetById(laboratory.LaboratoryId), laboratory);
        Assert.Equal(LaboratoryRepository.Instance.GetById(laboratory2.LaboratoryId), laboratory2);
    }

    private Laboratory CreateLaboratory(string title, int points)
    {
        ResultType result = _userDirector.CreateLaboratory(title, "Description", "Criteria", points);
        return Assert.IsType<SuccessResultWrapper<Laboratory>>(result).Value;
    }
}