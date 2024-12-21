using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Scenarios.Logout;

public class LogoutScenario : IScenario
{
    private readonly ICurrentUserService _currentUserService;

    public LogoutScenario(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public string Name => "Выход";

    public void Run()
    {
        _currentUserService.CurrentAccountNumber = null;
        AnsiConsole.MarkupLine("[green]Вы успешно вышли из системы.[/]");
    }
}