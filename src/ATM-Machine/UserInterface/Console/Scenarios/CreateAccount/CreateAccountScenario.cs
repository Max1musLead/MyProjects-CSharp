using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Scenarios.CreateAccount;

public class CreateAccountScenario : IScenario
{
    private readonly IAccountService _accountService;

    public CreateAccountScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Создать счёт";

    public void Run()
    {
        long accountNumber = AnsiConsole.Ask<long>("Введите желаемый номер счёта (минимум 8 символов):");
        string pin = AnsiConsole.Prompt(
            new TextPrompt<string>("Введите PIN (минимум 4 символа:")
                .Secret());

        ResultType result = _accountService.CreateAccount(accountNumber, pin).GetAwaiter().GetResult();

        if (result.IsSuccess)
        {
            AnsiConsole.MarkupLine("[green]Счёт создан успешно![/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Ошибка: {((ErrorResult)result).ErrorMessage}[/]");
        }
    }
}