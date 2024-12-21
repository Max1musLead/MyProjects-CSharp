using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.Routes;
using Itmo.ObjectOrientedProgramming.Lab1.Trains;
using Xunit;

namespace Lab1.Tests;

public class TestScenarios
{
    private const double Precision = 0.1;

    private const double Mass = 1000.0;

    private const double MaxForce = 5000.0;

    [Fact]
    public void TestScenario1_Success()
    {
        // Arrange
        IRouteSegment poweredPath = CreateAndGetPoweredPath(449.0, 1000.0);
        IRouteSegment regularPath = CreateAndGetRegularPath(2000.0);
        const double routeMaxAllowSpeed = 30.0;

        var segments = new List<IRouteSegment> { poweredPath, regularPath };

        // Act
        ResultType routeResultType = CreateRouteAndTryPass(segments, routeMaxAllowSpeed);

        // Assert
        Assert.IsType<SuccessWithTime>(routeResultType);
    }

    [Fact]
    public void TestScenario2_Failure_MaxSpeed()
    {
        // Arrange
        IRouteSegment poweredPath = CreateAndGetPoweredPath(5000.0, 1000.0);
        IRouteSegment regularPath = CreateAndGetRegularPath(2000.0);
        const double routeMaxAllowSpeed = 30.0;

        var segments = new List<IRouteSegment> { poweredPath, regularPath };

        // Act
        ResultType routeResultType = CreateRouteAndTryPass(segments, routeMaxAllowSpeed);

        // Assert
        Assert.IsType<ErrorInvalidSpeed>(routeResultType);
    }

    [Fact]
    public void TestScenario3_Success_WithStation()
    {
        // Arrange
        IRouteSegment poweredPath1 = CreateAndGetPoweredPath(449.0, 1000.0);
        IRouteSegment regularPath1 = CreateAndGetRegularPath(2000.0);
        IRouteSegment station = CreateAndGetStation(30.0, 300.0);
        IRouteSegment regularPath2 = CreateAndGetRegularPath(2000.0);
        const double routeMaxAllowSpeed = 30.0;

        var segments = new List<IRouteSegment> { poweredPath1, regularPath1, station, regularPath2 };

        // Act
        ResultType routeResultType = CreateRouteAndTryPass(segments, routeMaxAllowSpeed);

        // Assert
        Assert.IsType<SuccessWithTime>(routeResultType);
    }

    [Fact]
    public void TestScenario4_Failure_StationMaxSpeed()
    {
        // Arrange
        IRouteSegment poweredPath = CreateAndGetPoweredPath(600.0, 1000.0);
        IRouteSegment station = CreateAndGetStation(30.0, 300.0);
        IRouteSegment regularPath = CreateAndGetRegularPath(2000.0);
        const double routeMaxAllowSpeed = 300.0;

        var segments = new List<IRouteSegment> { poweredPath, station, regularPath };

        // Act
        ResultType routeResultType = CreateRouteAndTryPass(segments, routeMaxAllowSpeed);

        // Assert
        Assert.IsType<ErrorInvalidSpeed>(routeResultType);
    }

    [Fact]
    public void TestScenario5_Failure_MaxSpeed()
    {
        // Arrange
        IRouteSegment poweredPath = CreateAndGetPoweredPath(500.0, 1000.0);
        IRouteSegment station = CreateAndGetStation(35.0, 300.0);
        IRouteSegment regularPath = CreateAndGetRegularPath(2000.0);
        const double routeMaxAllowSpeed = 30.0;

        var segments = new List<IRouteSegment> { poweredPath, station, regularPath };

        // Act
        ResultType result = CreateRouteAndTryPass(segments, routeMaxAllowSpeed);

        // Assert
        Assert.IsType<ErrorInvalidSpeed>(result);
    }

    [Fact]
    public void TestScenario6_Success_ComplexRoute()
    {
        // Arrange
        // Ускорение до 30 V = sqrt(v0^2 + 2*F*s/m)
        IRouteSegment poweredPath1 = CreateAndGetPoweredPath(450.0, 1000.0);
        IRouteSegment regularPath1 = CreateAndGetRegularPath(200.0);

        // Замедление до 19,7
        IRouteSegment poweredPath2 = CreateAndGetPoweredPath(-255.0, 1000.0);
        IRouteSegment station = CreateAndGetStation(20.0, 300.0);
        IRouteSegment regularPath2 = CreateAndGetRegularPath(200.0);

        // Ускорение до 41
        IRouteSegment poweredPath3 = CreateAndGetPoweredPath(5000.0, 130.0);
        IRouteSegment regularPath3 = CreateAndGetRegularPath(200.0);

        // Замедление до 29,6
        IRouteSegment poweredPath4 = CreateAndGetPoweredPath(-1000.0, 400.0);
        const double routeMaxAllowSpeed = 30.0;

        var segments = new List<IRouteSegment>
        {
            poweredPath1,
            regularPath1,
            poweredPath2,
            station,
            regularPath2,
            poweredPath3,
            regularPath3,
            poweredPath4,
        };

        // Act
        ResultType routeResultType = CreateRouteAndTryPass(segments, routeMaxAllowSpeed);

        // Assert
        Assert.IsType<SuccessWithTime>(routeResultType);
    }

    [Fact]
    public void TestScenario7_Failure_NoSpeed()
    {
        // Arrange
        IRouteSegment regularPath = CreateAndGetRegularPath(2000.0);
        const double routeMaxAllowSpeed = 30.0;

        var segments = new List<IRouteSegment> { regularPath };

        // Act
        ResultType routeResultType = CreateRouteAndTryPass(segments, routeMaxAllowSpeed);

        // Assert
        Assert.IsType<ErrorInvalidSpeed>(routeResultType);
    }

    [Fact]
    public void TestScenario8_Failure_ZeroSpeed()
    {
        // Arrange
        IRouteSegment poweredPath1 = CreateAndGetPoweredPath(500.0, 1000.0);
        IRouteSegment poweredPath2 = CreateAndGetPoweredPath(-1000.0, 1000.0);
        const double routeMaxAllowSpeed = 300.0;

        var segments = new List<IRouteSegment> { poweredPath1, poweredPath2 };

        // Act
        ResultType routeResultType = CreateRouteAndTryPass(segments, routeMaxAllowSpeed);

        // Assert
        Assert.IsType<ErrorInvalidSpeed>(routeResultType);
    }

    private Train CreateAndGetTrain()
    {
        ResultType trainResult = Train.Create(Mass, MaxForce, Precision);
        TrainSuccessWrapperInstance trainSuccess = Assert.IsType<TrainSuccessWrapperInstance>(trainResult);
        Train train = Assert.IsType<Train>(trainSuccess.Instance);
        return train;
    }

    private IRouteSegment CreateAndGetPoweredPath(double force, double distance)
    {
        ResultType pathResult = PoweredMagneticPath.Create(force, distance);
        RouteSegmentSuccessWrapperInstance pathSuccess = Assert.IsType<RouteSegmentSuccessWrapperInstance>(pathResult);
        IRouteSegment path = Assert.IsType<PoweredMagneticPath>(pathSuccess.Instance);
        return path;
    }

    private IRouteSegment CreateAndGetRegularPath(double distance)
    {
        ResultType pathResult = RegularMagneticPath.Create(distance);
        RouteSegmentSuccessWrapperInstance pathSuccess = Assert.IsType<RouteSegmentSuccessWrapperInstance>(pathResult);
        IRouteSegment path = Assert.IsType<RegularMagneticPath>(pathSuccess.Instance);
        return path;
    }

    private IRouteSegment CreateAndGetStation(double maxAllowSpeed, double changePassengersTime)
    {
        ResultType stationResult = Station.Create(maxAllowSpeed, changePassengersTime);
        RouteSegmentSuccessWrapperInstance stationSuccess = Assert.IsType<RouteSegmentSuccessWrapperInstance>(stationResult);
        IRouteSegment station = Assert.IsType<Station>(stationSuccess.Instance);
        return station;
    }

    private ResultType CreateRouteAndTryPass(List<IRouteSegment> segments, double maxAllowSpeed)
    {
        var route = new Route(segments, maxAllowSpeed);
        Train train = CreateAndGetTrain();
        return route.TryPassRoute(train);
    }
}