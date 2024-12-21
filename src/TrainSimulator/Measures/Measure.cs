namespace Itmo.ObjectOrientedProgramming.Lab1.Measures;

public abstract record Measure
{
    public double Value { get; }

    protected Measure(double value)
    {
        if (value <= 0.0)
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
        Value = value;
    }
}