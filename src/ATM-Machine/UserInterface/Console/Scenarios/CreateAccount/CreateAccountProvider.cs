using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Scenarios.CreateAccount;

public class CreateAccountProvider : IScenarioProvider
{
    private readonly IAccountService _accountService;
    private readonly ICurrentUserService _currentUserService;

    public CreateAccountProvider(IAccountService accountService, ICurrentUserService currentUserService)
    {
        _accountService = accountService;
        _currentUserService = currentUserService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.CurrentAccountNumber is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new CreateAccountScenario(_accountService);
        return true;
    }
}