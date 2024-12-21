using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypeWrappers;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Commands;

public class ViewBalanceCommand : ICommand
{
    private readonly IAccountService _accountService;

    private readonly ICurrentUserService _currentUserService;

    public ViewBalanceCommand(IAccountService accountService, ICurrentUserService currentUserService)
    {
        _accountService = accountService;
        _currentUserService = currentUserService;
    }

    public string Name => "Просмотр баланса";

    public async Task Execute()
    {
        ArgumentNullException.ThrowIfNull(_currentUserService.CurrentAccountNumber);

        ResultTypeWrapper<decimal> result = await _accountService.GetAccountBalance(_currentUserService.CurrentAccountNumber.Value).ConfigureAwait(false);

        if (result.IsSuccess)
        {
            AnsiConsole.MarkupLine($"Ваш баланс: [green]{((SuccessResultWrapper<decimal>)result).Data}[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Ошибка: {((NotFoundWrapper<decimal>)result).ErrorMessage}[/]");
        }
    }
}