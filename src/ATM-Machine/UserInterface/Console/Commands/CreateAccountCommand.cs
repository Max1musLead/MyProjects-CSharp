using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Commands;

public class CreateAccountCommand : ICommand
{
    private readonly IAccountService _accountService;

    public CreateAccountCommand(IAccountService userService)
    {
        _accountService = userService;
    }

    public string Name => "Создать счёт";

    public async Task Execute()
    {
        long accountNumber = AnsiConsole.Ask<long>("Введите желаемый номер счёта, но не менее 8 символов:");
        string pin = AnsiConsole.Prompt(
            new TextPrompt<string>("Введите PIN, не менее 4 символов:")
                .Secret());

        ResultType result = await _accountService.CreateAccount(accountNumber, pin).ConfigureAwait(false);

        if (result.IsSuccess)
        {
            AnsiConsole.MarkupLine("[green]Счёт создан успешно.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Ошибка: {((ErrorResult)result).ErrorMessage}[/]");
        }
    }
}