using Itmo.ObjectOrientedProgramming.Lab1.Measures;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.Trains;

namespace Itmo.ObjectOrientedProgramming.Lab1.Routes;

public interface IRouteSegment
{
    ResultType TryPass(Train train);
}

public interface IDistanceRouteSegment : IRouteSegment
{
    public Measure Distance { get; }
}