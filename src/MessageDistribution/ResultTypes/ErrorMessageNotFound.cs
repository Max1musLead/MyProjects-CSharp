namespace Itmo.ObjectOrientedProgramming.Lab3.ResultTypes;

public sealed record ErrorMessageNotFound(string ErrorMessage) : ResultType(false);