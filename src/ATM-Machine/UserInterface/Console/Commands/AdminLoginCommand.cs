using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Commands;

public class AdminLoginCommand : ICommand
{
    private readonly IUserService _userService;

    private readonly ICurrentUserService _currentUserService;

    public AdminLoginCommand(IUserService userService, ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }

    public string Name => "Вход администратор";

    public Task Execute()
    {
        string password = AnsiConsole.Prompt(
            new TextPrompt<string>("Введите системный пароль:")
                .Secret());

        if (_userService.AuthenticateAdmin(password))
        {
            _currentUserService.CurrentAccountNumber = 0;
            AnsiConsole.MarkupLine("[green]Вход администратора выполнен успешно![/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Неверный пароль. Завершение работы.[/]");
            Environment.Exit(0);
        }

        return Task.CompletedTask;
    }
}