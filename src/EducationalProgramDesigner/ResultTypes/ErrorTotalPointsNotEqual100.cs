namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public sealed record ErrorTotalPointsNotEqual100(string ErrorMessage) : ResultType(false);