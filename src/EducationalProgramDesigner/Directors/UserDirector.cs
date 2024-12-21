using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.EvaluationFormat;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Directors;

public class UserDirector
{
    private readonly User _user;

    public UserDirector(User user)
    {
        _user = user;
    }

    public ResultType CreateLaboratory(string title, string description, string criteria, int points, Guid? baseLaboratoryId = null)
    {
        var labDirector = new LaboratoryDirector();
        ResultType result = labDirector.Construct(title, description, criteria, points, _user, baseLaboratoryId);

        if (!result.IsSuccess)
            return result;

        Laboratory laboratory = ((SuccessResultWrapper<Laboratory>)result).Value;
        LaboratoryRepository.Instance.Add(laboratory);

        return result;
    }

    public ResultType CreateLectureMaterial(string title, string description, string content, Guid? baseLectureId = null)
    {
        var lectureDirector = new LectureMaterialDirector();
        ResultType result = lectureDirector.Construct(title, description, content, _user, baseLectureId);

        if (!result.IsSuccess)
            return result;

        LectureMaterial lectureMaterial = ((SuccessResultWrapper<LectureMaterial>)result).Value;
        LectureMaterialRepository.Instance.Add(lectureMaterial);

        return result;
    }

    public ResultType CreateSubject(string name, IList<Laboratory> laboratories, IList<LectureMaterial> lectureMaterials, IEvaluationFormat evaluationFormat, Guid? baseSubjectId = null)
    {
        var subjectDirector = new SubjectDirector();
        ResultType result = subjectDirector.Construct(name, laboratories, lectureMaterials, _user, evaluationFormat, baseSubjectId);

        if (!result.IsSuccess)
            return result;

        Subject subject = ((SuccessResultWrapper<Subject>)result).Value;
        SubjectRepository.Instance.Add(subject);

        return result;
    }

    public ResultType CreateEducationalProgram(string title, User responsiblePerson, User programManager, IList<Semester> semesters)
    {
        var programDirector = new EducationalProgramDirector();
        ResultType result = programDirector.Construct(title, responsiblePerson, programManager, semesters);

        if (!result.IsSuccess)
            return result;

        EducationalProgram educationalProgram = ((SuccessResultWrapper<EducationalProgram>)result).Value;
        EducationalProgramRepository.Instance.Add(educationalProgram);

        return result;
    }
}