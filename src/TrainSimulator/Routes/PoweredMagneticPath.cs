using Itmo.ObjectOrientedProgramming.Lab1.Measures;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.Trains;

namespace Itmo.ObjectOrientedProgramming.Lab1.Routes;

public class PoweredMagneticPath : IDistanceRouteSegment
{
    public Measure Distance { get; }

    public double Force { get; }

    private PoweredMagneticPath(double force, double distance)
    {
        Distance = new Distance(distance);
        Force = force;
    }

    public static ResultType Create(double force, double distance)
    {
        return new RouteSegmentSuccessWrapperInstance(new PoweredMagneticPath(force, distance));
    }

    public ResultType TryPass(Train train)
    {
        ResultType applyForce = train.TryApplyForce(Force);
        if (!applyForce.IsSuccess)
            return applyForce;

        ResultType passTimeResult = train.CalculatePassTime(Distance.Value);
        if (!passTimeResult.IsSuccess)
            return passTimeResult;

        if (passTimeResult is not SuccessWithTime time)
            return new RouteSegmentErrorPass("Unknown error passing a segment.");

        train.ResetAcceleration();
        return new SuccessWithTime(time.Time);
    }
}