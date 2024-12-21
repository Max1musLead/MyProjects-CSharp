using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.EvaluationFormat;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Directors;

public class SubjectDirector
{
    private readonly Subject.SubjectBuilder _builder;

    public SubjectDirector()
    {
        _builder = new Subject.SubjectBuilder();
    }

    public ResultType Construct(string name, IList<Laboratory> laboratories, IList<LectureMaterial> lectureMaterials, User author, IEvaluationFormat evaluationFormat, Guid? baseSubjectId)
    {
        return _builder
            .SetName(name)
            .SetLaboratories(laboratories)
            .SetLectureMaterials(lectureMaterials)
            .SetAuthor(author)
            .SetEvaluationFormat(evaluationFormat)
            .SetBaseSubjectId(baseSubjectId)
            .Build();
    }
}