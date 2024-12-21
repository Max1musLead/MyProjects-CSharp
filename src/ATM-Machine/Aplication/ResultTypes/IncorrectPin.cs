namespace Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypes;

public sealed record IncorrectPin(string ErrorMessage) : ResultType(false);