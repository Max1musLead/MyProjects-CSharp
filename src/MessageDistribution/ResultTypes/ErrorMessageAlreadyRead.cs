namespace Itmo.ObjectOrientedProgramming.Lab3.ResultTypes;

public sealed record ErrorMessageAlreadyRead(string ErrorMessage) : ResultType(false);