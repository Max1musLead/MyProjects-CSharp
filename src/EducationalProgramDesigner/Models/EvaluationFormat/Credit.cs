namespace Itmo.ObjectOrientedProgramming.Lab2.Models.EvaluationFormat;

public class Credit : IEvaluationFormat
{
    public int Points { get; }

    public Credit(int minimumPoints)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(minimumPoints);
        Points = minimumPoints;
    }
}