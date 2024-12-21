using Itmo.ObjectOrientedProgramming.Lab1.Trains;

namespace Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

public sealed record TrainSuccessWrapperInstance(Train Instance) : ResultType(true);