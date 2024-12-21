using Itmo.ObjectOrientedProgramming.Lab1.Routes;

namespace Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

public sealed record RouteSegmentSuccessWrapperInstance(IRouteSegment Instance) : ResultType(true);