using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class EducationalProgram
{
    public Guid EducationalProgramId { get; }

    public string Title { get; }

    public IList<Semester> Semesters { get; }

    public User ResponsiblePerson { get; }

    public User ProgramManager { get; }

    private EducationalProgram(string title, IList<Semester> semesters, User responsiblePerson, User programManager)
    {
        EducationalProgramId = Guid.NewGuid();
        Title = title;
        Semesters = semesters;
        ResponsiblePerson = responsiblePerson;
        ProgramManager = programManager;
    }

    public static EducationalProgramBuilder Builder => new EducationalProgramBuilder();

    public class EducationalProgramBuilder
    {
        private string? _title;
        private User? _responsiblePerson;
        private User? _programManager;
        private IList<Semester>? _semesters;

        public EducationalProgramBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public EducationalProgramBuilder SetResponsiblePerson(User responsiblePerson)
        {
            _responsiblePerson = responsiblePerson;
            return this;
        }

        public EducationalProgramBuilder SetProgramManager(User programManager)
        {
            _programManager = programManager;
            return this;
        }

        public EducationalProgramBuilder SetSemesters(IList<Semester> semesters)
        {
            _semesters = semesters;
            return this;
        }

        public ResultType Build()
        {
            ArgumentNullException.ThrowIfNull(_title);
            ArgumentNullException.ThrowIfNull(_responsiblePerson);
            ArgumentNullException.ThrowIfNull(_programManager);
            ArgumentNullException.ThrowIfNull(_semesters);

            return new SuccessResultWrapper<EducationalProgram>(new EducationalProgram(_title, _semesters, _responsiblePerson, _programManager));
        }
    }

    public void AddSubjectToSemester(Subject subject, int semesterNumber)
    {
        Semester? semester = Semesters.FirstOrDefault(s => s.Number == semesterNumber);
        if (semester == null)
        {
            semester = new Semester(semesterNumber);
            Semesters.Add(semester);
        }

        semester.AddSubject(subject);
    }
}