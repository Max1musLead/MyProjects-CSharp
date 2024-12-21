using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.DataAccess.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Lab5;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection
            .AddInfrastructureDataAccess(config =>
        {
            config.Host = "localhost";
            config.Port = 6432;
            config.Username = "postgres";
            config.Password = "postgres";
            config.Database = "postgres";
            config.SslMode = "Prefer";
        })
            .AddApplication()
            .AddUserInterfaceConsole();

        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        using IServiceScope scope = serviceProvider.CreateScope();

        scope.UseInfrastructureDataAccess();

        ScenarioRunner scenarioRunner = scope.ServiceProvider
            .GetRequiredService<ScenarioRunner>();

        while (true)
        {
            scenarioRunner.Run();
        }
    }
}