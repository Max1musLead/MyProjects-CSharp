namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class Semester
{
    public int Number { get; }

    public IList<Subject> Subjects { get; }

    public Semester(int number)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(number);

        Number = number;
        Subjects = new List<Subject>();
    }

    public Semester(IList<Subject> subjects)
    {
        Subjects = subjects;
    }

    public void AddSubject(Subject subject)
    {
        Subjects.Add(subject);
    }
}