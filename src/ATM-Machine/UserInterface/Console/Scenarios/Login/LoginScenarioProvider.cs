using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Scenarios.Login;

public class LoginScenarioProvider : IScenarioProvider
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;

    public LoginScenarioProvider(IUserService userService, ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.CurrentAccountNumber is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new LoginScenario(_userService, _currentUserService);
        return true;
    }
}