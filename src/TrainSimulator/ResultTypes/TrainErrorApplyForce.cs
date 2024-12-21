namespace Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

public sealed record TrainErrorApplyForce() : ResultType(false)
{
    public string ErrorMessage { get; } = "Maximum power is exceeded";
}