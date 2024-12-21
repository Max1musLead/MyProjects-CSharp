namespace Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypes;

public sealed record NotFound(string ErrorMessage) : ResultType(false);