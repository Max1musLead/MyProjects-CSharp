namespace Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypes;

public sealed record InsufficientFunds(string ErrorMessage) : ResultType(false);