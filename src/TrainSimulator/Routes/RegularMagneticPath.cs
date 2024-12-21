using Itmo.ObjectOrientedProgramming.Lab1.Measures;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.Trains;

namespace Itmo.ObjectOrientedProgramming.Lab1.Routes;

public class RegularMagneticPath : IDistanceRouteSegment
{
    public Measure Distance { get; }

    private RegularMagneticPath(double distance)
    {
        Distance = new Distance(distance);
    }

    public static ResultType Create(double distance)
    {
        return new RouteSegmentSuccessWrapperInstance(new RegularMagneticPath(distance));
    }

    public ResultType TryPass(Train train)
    {
        ResultType passTimeResult = train.CalculatePassTime(Distance.Value);
        if (!passTimeResult.IsSuccess)
            return passTimeResult;

        if (passTimeResult is not SuccessWithTime time)
            return new RouteSegmentErrorPass("Unknown error passing a segment.");

        return new SuccessWithTime(time.Time);
    }
}