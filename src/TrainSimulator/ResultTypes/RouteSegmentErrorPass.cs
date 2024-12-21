namespace Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

public sealed record RouteSegmentErrorPass(string ErrorMessage) : ResultType(false);