using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Scenarios.User;

public class UserMenuScenarioProvider : IScenarioProvider
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IAccountService _accountService;

    public UserMenuScenarioProvider(ICurrentUserService currentUserService, IAccountService accountService)
    {
        _currentUserService = currentUserService;
        _accountService = accountService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.CurrentAccountNumber is not null && _currentUserService.CurrentAccountNumber.Value != 0)
        {
            scenario = new UserMenuScenario(_currentUserService, _accountService);
            return true;
        }

        scenario = null;
        return false;
    }
}