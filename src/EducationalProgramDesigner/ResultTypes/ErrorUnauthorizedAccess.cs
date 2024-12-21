namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public sealed record ErrorUnauthorizedAccess(string ErrorMessage) : ResultType(false);