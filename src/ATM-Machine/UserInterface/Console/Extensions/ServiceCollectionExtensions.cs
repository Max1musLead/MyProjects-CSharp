using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Scenarios.CreateAccount;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Scenarios.Login;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Scenarios.Logout;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Scenarios.User;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserInterfaceConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, CreateAccountProvider>();
        collection.AddScoped<IScenarioProvider, LoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, UserMenuScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LogoutScenarioProvider>();

        return collection;
    }
}