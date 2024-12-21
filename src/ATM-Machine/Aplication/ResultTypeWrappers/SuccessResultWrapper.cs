namespace Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypeWrappers;

public sealed record SuccessResultWrapper<T>(T Data) : ResultTypeWrapper<T>(true);