using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Commands;

public class UserLoginCommand : ICommand
{
    private readonly IUserService _userService;

    private readonly ICurrentUserService _currentUserService;

    public UserLoginCommand(IUserService userService, ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }

    public string Name => "Вход пользователь";

    public async Task Execute()
    {
        long accountNumber = AnsiConsole.Ask<long>("Введите номер счёта:");
        string pin = AnsiConsole.Prompt(
            new TextPrompt<string>("Введите PIN:")
                .Secret());

        ResultType result = await _userService.AuthenticateUser(accountNumber, pin).ConfigureAwait(false);

        if (result.IsSuccess)
        {
            _currentUserService.CurrentAccountNumber = accountNumber;
            AnsiConsole.MarkupLine("[green]Вход выполнен успешно![/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Ошибка: {((IncorrectPin)result).ErrorMessage}[/]");
        }
    }
}