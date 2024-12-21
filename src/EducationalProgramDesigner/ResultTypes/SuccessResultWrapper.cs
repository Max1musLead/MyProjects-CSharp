namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public sealed record SuccessResultWrapper<T>(T Value) : ResultType(true);