using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Directors;

public class LectureMaterialDirector
{
    private readonly LectureMaterial.LectureMaterialBuilder _builder;

    public LectureMaterialDirector()
    {
        _builder = new LectureMaterial.LectureMaterialBuilder();
    }

    public ResultType Construct(string title, string description, string content, User author, Guid? baseLectureId)
    {
        return _builder
            .SetTitle(title)
            .SetDescription(description)
            .SetContent(content)
            .SetAuthor(author)
            .SetBaseLectureId(baseLectureId)
            .Build();
    }
}