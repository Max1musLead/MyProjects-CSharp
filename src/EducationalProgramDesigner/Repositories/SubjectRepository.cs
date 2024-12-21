using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class SubjectRepository
{
    private static readonly Lazy<SubjectRepository> Lazy;

    public static SubjectRepository Instance => Lazy.Value;

    private readonly Dictionary<Guid, Subject> _subjects;

    static SubjectRepository()
    {
        Lazy = new Lazy<SubjectRepository>(() => new SubjectRepository());
    }

    private SubjectRepository()
    {
        _subjects = new Dictionary<Guid, Subject>();
    }

    public void Add(Subject subject)
    {
        _subjects[subject.SubjectId] = subject;
    }

    public Subject? GetById(Guid subjectId)
    {
        _subjects.TryGetValue(subjectId, out Subject? subject);
        return subject;
    }
}