using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Directors;

public class EducationalProgramDirector
{
    private readonly EducationalProgram.EducationalProgramBuilder _builder;

    public EducationalProgramDirector()
    {
        _builder = new EducationalProgram.EducationalProgramBuilder();
    }

    public ResultType Construct(string title, User responsiblePerson, User programManager, IList<Semester> semesters)
    {
        return _builder
            .SetTitle(title)
            .SetResponsiblePerson(responsiblePerson)
            .SetProgramManager(programManager)
            .SetSemesters(semesters)
            .Build();
    }
}