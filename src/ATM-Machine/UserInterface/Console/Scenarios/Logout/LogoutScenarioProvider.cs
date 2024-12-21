using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Scenarios.Logout;

public class LogoutScenarioProvider : IScenarioProvider
{
    private readonly ICurrentUserService _currentUserService;

    public LogoutScenarioProvider(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.CurrentAccountNumber is not null)
        {
            scenario = new LogoutScenario(_currentUserService);
            return true;
        }

        scenario = null;
        return false;
    }
}