using Itmo.ObjectOrientedProgramming.Lab2.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class Laboratory : IPrototype<Laboratory>
{
    public Guid LaboratoryId { get; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public string Criteria { get; private set; }

    public int Points { get; }

    public User Author { get; }

    public Guid? BaseLaboratoryId { get; }

    private Laboratory(string title, string description, string criteria, int points, User author, Guid? baseLaboratoryId)
    {
        LaboratoryId = Guid.NewGuid();
        Title = title;
        Description = description;
        Criteria = criteria;
        Points = points;
        Author = author;
        BaseLaboratoryId = baseLaboratoryId;
    }

    public static LaboratoryBuilder Builder => new LaboratoryBuilder();

    public class LaboratoryBuilder
    {
        private string? _title;

        private string? _description;

        private string? _criteria;

        private int _points;

        private User? _author;

        private Guid? _baseLaboratoryId;

        public LaboratoryBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public LaboratoryBuilder SetDescription(string description)
        {
            _description = description;
            return this;
        }

        public LaboratoryBuilder SetCriteria(string criteria)
        {
            _criteria = criteria;
            return this;
        }

        public LaboratoryBuilder SetPoints(int points)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(points);
            _points = points;
            return this;
        }

        public LaboratoryBuilder SetAuthor(User author)
        {
            _author = author;
            return this;
        }

        public LaboratoryBuilder SetBaseLaboratoryId(Guid? baseLaboratoryId)
        {
            _baseLaboratoryId = baseLaboratoryId;
            return this;
        }

        public ResultType Build()
        {
            ArgumentNullException.ThrowIfNull(_title);
            ArgumentNullException.ThrowIfNull(_description);
            ArgumentNullException.ThrowIfNull(_criteria);
            ArgumentNullException.ThrowIfNull(_author);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(_points);

            return new SuccessResultWrapper<Laboratory>(new Laboratory(_title, _description, _criteria, _points, _author, _baseLaboratoryId));
        }
    }

    public ResultType TryChangeLaboratory(string title, string description, string criteria, User user)
    {
        if (Author != user)
            return new ErrorUnauthorizedAccess("Only the author can change the laboratory.");

        Title = title;
        Description = description;
        Criteria = criteria;

        return new SuccessResult();
    }

    public Laboratory Clone(User newAuthor)
    {
        var userDirector = new UserDirector(newAuthor);
        Laboratory clone = ((SuccessResultWrapper<Laboratory>)userDirector.CreateLaboratory(Title, Description, Criteria, Points, LaboratoryId)).Value;
        return clone;
    }
}