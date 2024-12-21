namespace Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

public sealed record ErrorInvalidSpeed(string ErrorMessage) : ResultType(false);