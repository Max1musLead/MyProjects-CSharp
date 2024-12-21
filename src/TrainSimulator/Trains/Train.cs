using Itmo.ObjectOrientedProgramming.Lab1.Measures;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Trains;

public class Train
{
    public double Speed { get; private set; }

    public double Acceleration { get; private set; }

    public double Precision { get; }

    public double MaxForce { get; }

    public Measure Mass { get; }

    private Train(double mass, double maxForce, double precision)
    {
        Acceleration = 0;
        Speed = 0;
        MaxForce = maxForce;
        Precision = precision;
        Mass = new Mass(mass);
    }

    public static ResultType Create(double mass, double maxForce, double precision)
    {
        return new TrainSuccessWrapperInstance(new Train(mass, maxForce, precision));
    }

    public void ResetAcceleration()
    {
        Acceleration = 0.0;
    }

    public ResultType TryApplyForce(double force)
    {
        if (Math.Abs(force) > MaxForce)
        {
            return new TrainErrorApplyForce();
        }

        Acceleration = force / Mass.Value;
        return new Success();
    }

    public ResultType CalculatePassTime(double distance)
    {
        const double noSpeed = 0.0;

        if (Math.Abs(Speed) <= noSpeed && Math.Abs(Acceleration) <= noSpeed)
        {
            return new ErrorInvalidSpeed("It is impossible to calculate time.");
        }

        double totalTime = 0.0;
        double remainingDistance = distance;
        double currentSpeed = Speed;
        double currentAcceleration = Acceleration;

        while (remainingDistance >= 0.0)
        {
            double newSpeed = currentSpeed + (currentAcceleration * Precision);

            double traveledDistance = newSpeed * Precision;

            remainingDistance -= traveledDistance;
            totalTime += Precision;
            currentSpeed = newSpeed;

            if (currentSpeed <= noSpeed)
            {
                return new ErrorInvalidSpeed("The train stopped");
            }
        }

        Speed = currentSpeed;
        return new SuccessWithTime(totalTime);
    }
}