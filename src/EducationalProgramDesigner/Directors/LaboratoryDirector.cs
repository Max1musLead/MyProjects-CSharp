using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Directors;

public class LaboratoryDirector
{
    private readonly Laboratory.LaboratoryBuilder _builder;

    public LaboratoryDirector()
    {
        _builder = new Laboratory.LaboratoryBuilder();
    }

    public ResultType Construct(string title, string description, string criteria, int points, User author, Guid? baseLaboratoryId)
    {
        return _builder
            .SetTitle(title)
            .SetDescription(description)
            .SetCriteria(criteria)
            .SetPoints(points)
            .SetAuthor(author)
            .SetBaseLaboratoryId(baseLaboratoryId)
            .Build();
    }
}
