using Itmo.ObjectOrientedProgramming.Lab1.Measures;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.Trains;

namespace Itmo.ObjectOrientedProgramming.Lab1.Routes;

public class Station : IRouteSegment
{
    public double MaxAllowSpeed { get; }

    public Measure ChangePassengersTime { get; }

    private Station(double maxAllowSpeed, double changePassengersTime)
    {
        ChangePassengersTime = new TimeMeasure(changePassengersTime);
        MaxAllowSpeed = maxAllowSpeed;
    }

    public static ResultType Create(double maxAllowSpeed, double changePassengersTime)
    {
        if (maxAllowSpeed < 0.0)
        {
            return new RouteSegmentErrorPass("The maximum allow speed cannot be negative.");
        }

        return new RouteSegmentSuccessWrapperInstance(new Station(maxAllowSpeed, changePassengersTime));
    }

    public ResultType TryPass(Train train)
    {
        if (Math.Abs(train.Speed) > Math.Abs(MaxAllowSpeed))
        {
            return new ErrorInvalidSpeed("The speed is higher than the maximum allowed.");
        }

        return new SuccessWithTime(ChangePassengersTime.Value);
    }
}