using Itmo.ObjectOrientedProgramming.Lab2.Directors;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class LectureMaterial : IPrototype<LectureMaterial>
{
    public Guid LectureId { get; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public string Content { get; private set; }

    public User Author { get; }

    public Guid? BaseLectureId { get; }

    private LectureMaterial(string title, string description, string content, User author, Guid? baseLectureId)
    {
        LectureId = Guid.NewGuid();
        Title = title;
        Description = description;
        Content = content;
        Author = author;
        BaseLectureId = baseLectureId;
    }

    public static LectureMaterialBuilder Builder => new LectureMaterialBuilder();

    public class LectureMaterialBuilder
    {
        private string? _title;
        private string? _description;
        private string? _content;
        private User? _author;
        private Guid? _baseLectureId;

        public LectureMaterialBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public LectureMaterialBuilder SetDescription(string description)
        {
            _description = description;
            return this;
        }

        public LectureMaterialBuilder SetContent(string content)
        {
            _content = content;
            return this;
        }

        public LectureMaterialBuilder SetAuthor(User author)
        {
            _author = author;
            return this;
        }

        public LectureMaterialBuilder SetBaseLectureId(Guid? baseLectureId)
        {
            _baseLectureId = baseLectureId;
            return this;
        }

        public ResultType Build()
        {
            ArgumentNullException.ThrowIfNull(_title);
            ArgumentNullException.ThrowIfNull(_description);
            ArgumentNullException.ThrowIfNull(_content);
            ArgumentNullException.ThrowIfNull(_author);

            return new SuccessResultWrapper<LectureMaterial>(new LectureMaterial(_title, _description, _content, _author, _baseLectureId));
        }
    }

    public ResultType TryChangeLectureMaterial(string title, string description, string content, User user)
    {
        if (Author != user)
            return new ErrorUnauthorizedAccess("Only the author can change the lecture material.");

        Title = title;
        Description = description;
        Content = content;

        return new SuccessResult();
    }

    public LectureMaterial Clone(User newAuthor)
    {
        var userDirector = new UserDirector(newAuthor);
        LectureMaterial clone = ((SuccessResultWrapper<LectureMaterial>)userDirector.CreateLectureMaterial(Title, Description, Content, LectureId)).Value;
        return clone;
    }
}