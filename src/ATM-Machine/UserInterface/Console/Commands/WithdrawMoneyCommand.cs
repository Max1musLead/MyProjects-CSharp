using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Commands;

public class WithdrawMoneyCommand : ICommand
{
    private readonly IAccountService _accountService;
    private readonly ICurrentUserService _currentUserService;

    public WithdrawMoneyCommand(IAccountService accountService, ICurrentUserService currentUserService)
    {
        _accountService = accountService;
        _currentUserService = currentUserService;
    }

    public string Name => "Снятие денег";

    public async Task Execute()
    {
        ArgumentNullException.ThrowIfNull(_currentUserService.CurrentAccountNumber);

        decimal amount = AnsiConsole.Ask<decimal>("Введите сумму для снятия:");
        ResultType result = await _accountService.WithdrawMoney(_currentUserService.CurrentAccountNumber.Value, amount).ConfigureAwait(false);

        if (result.IsSuccess)
        {
            AnsiConsole.MarkupLine("[green]Операция успешна.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Ошибка: {((InsufficientFunds)result).ErrorMessage}[/]");
        }
    }
}