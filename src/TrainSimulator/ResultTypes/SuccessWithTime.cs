namespace Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

public sealed record SuccessWithTime(double Time) : ResultType(true);