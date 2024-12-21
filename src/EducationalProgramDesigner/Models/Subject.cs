using Itmo.ObjectOrientedProgramming.Lab2.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.Models.EvaluationFormat;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class Subject : IPrototype<Subject>
{
    public Guid SubjectId { get; }

    public string Name { get; private set; }

    public IList<Laboratory> LaboratoryList { get; }

    public IList<LectureMaterial> LectureMaterialList { get; private set; }

    public User Author { get; }

    public IEvaluationFormat EvaluationFormat { get; }

    public Guid? BaseSubjectId { get; }

    private Subject(string name, IList<Laboratory> laboratoryList, IList<LectureMaterial> lectureMaterialList, User author, IEvaluationFormat evaluationFormat, Guid? baseSubjectId)
    {
        SubjectId = Guid.NewGuid();
        Name = name;
        LaboratoryList = laboratoryList;
        LectureMaterialList = lectureMaterialList;
        Author = author;
        EvaluationFormat = evaluationFormat;
        BaseSubjectId = baseSubjectId;
    }

    public static SubjectBuilder Builder => new SubjectBuilder();

    public class SubjectBuilder
    {
        private string? _name;
        private IList<Laboratory>? _laboratories;
        private IList<LectureMaterial>? _lectureMaterials;
        private User? _author;
        private IEvaluationFormat? _evaluationFormat;
        private Guid? _baseSubjectId;

        public SubjectBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public SubjectBuilder SetLaboratories(IList<Laboratory> laboratories)
        {
            _laboratories = laboratories;
            return this;
        }

        public SubjectBuilder SetLectureMaterials(IList<LectureMaterial> lectureMaterials)
        {
            _lectureMaterials = lectureMaterials;
            return this;
        }

        public SubjectBuilder SetAuthor(User author)
        {
            _author = author;
            return this;
        }

        public SubjectBuilder SetEvaluationFormat(IEvaluationFormat evaluationFormat)
        {
            _evaluationFormat = evaluationFormat;
            return this;
        }

        public SubjectBuilder SetBaseSubjectId(Guid? baseSubjectId)
        {
            _baseSubjectId = baseSubjectId;
            return this;
        }

        public ResultType Build()
        {
            ArgumentNullException.ThrowIfNull(_name);
            ArgumentNullException.ThrowIfNull(_laboratories);
            ArgumentNullException.ThrowIfNull(_lectureMaterials);
            ArgumentNullException.ThrowIfNull(_author);
            ArgumentNullException.ThrowIfNull(_evaluationFormat);

            var subject = new Subject(_name, _laboratories, _lectureMaterials, _author, _evaluationFormat, _baseSubjectId);
            ResultType validationResult = subject.ValidateTotalPoints();

            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            return new SuccessResultWrapper<Subject>(subject);
        }
    }

    public ResultType TryChangeSubject(string name, IList<LectureMaterial> lectureMaterialList, User user)
    {
        if (Author != user)
            return new ErrorUnauthorizedAccess("Only the author can change the subject.");

        Name = name;
        LectureMaterialList = lectureMaterialList;

        return new SuccessResult();
    }

    public Subject Clone(User newAuthor)
    {
        var userDirector = new UserDirector(newAuthor);
        var clonedLaboratories = LaboratoryList.Select(lab => lab.Clone(newAuthor)).ToList();
        var clonedLectureMaterials = LectureMaterialList.Select(lec => lec.Clone(newAuthor)).ToList();

        Subject clone = ((SuccessResultWrapper<Subject>)userDirector.CreateSubject(Name, clonedLaboratories, clonedLectureMaterials, EvaluationFormat, SubjectId)).Value;

        return clone;
    }

    private ResultType ValidateTotalPoints()
    {
        int totalPoints = LaboratoryList.Sum(lab => lab.Points);
        totalPoints += EvaluationFormat.Points;

        if (totalPoints != 100)
        {
            return new ErrorTotalPointsNotEqual100("The total number of points should be 100.");
        }

        return new SuccessResult();
    }
}