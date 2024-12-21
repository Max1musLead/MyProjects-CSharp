namespace Itmo.ObjectOrientedProgramming.Lab2.Models.EvaluationFormat;

public class Exam : IEvaluationFormat
{
    public int Points { get; }

    public Exam(int points)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(points);
        Points = points;
    }
}