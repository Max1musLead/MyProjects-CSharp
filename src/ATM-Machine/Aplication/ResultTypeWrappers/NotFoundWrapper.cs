namespace Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypeWrappers;

public sealed record NotFoundWrapper<T>(string ErrorMessage) : ResultTypeWrapper<T>(false);