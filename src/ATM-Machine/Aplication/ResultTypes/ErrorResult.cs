namespace Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypes;

public sealed record ErrorResult(string ErrorMessage) : ResultType(false);