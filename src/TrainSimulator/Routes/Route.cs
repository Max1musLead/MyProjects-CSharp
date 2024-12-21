using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.Trains;

namespace Itmo.ObjectOrientedProgramming.Lab1.Routes;

public class Route
{
    public IReadOnlyList<IRouteSegment> Segments { get; }

    public double MaxAllowSpeed { get; }

    public Route(IReadOnlyList<IRouteSegment> segments, double maxAllowSpeed)
    {
        Segments = segments;
        MaxAllowSpeed = maxAllowSpeed;
    }

    public ResultType TryPassRoute(Train train)
    {
        double totalTime = 0.0;

        foreach (IRouteSegment segment in Segments)
        {
            ResultType segmentTimeResult = segment.TryPass(train);
            if (!segmentTimeResult.IsSuccess)
            {
                return segmentTimeResult;
            }

            if (segmentTimeResult is SuccessWithTime success)
            {
                totalTime += success.Time;
            }
        }

        return Math.Abs(train.Speed) > MaxAllowSpeed
            ? new ErrorInvalidSpeed("The speed is higher than the maximum allowed.")
            : new SuccessWithTime(totalTime);
    }
}