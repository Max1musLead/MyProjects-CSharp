using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypeWrappers;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Commands;

public class ViewTransactionHistoryCommand : ICommand
{
    private readonly IAccountService _accountService;
    private readonly ICurrentUserService _currentUserService;

    public ViewTransactionHistoryCommand(IAccountService accountService, ICurrentUserService currentUserService)
    {
        _accountService = accountService;
        _currentUserService = currentUserService;
    }

    public string Name => "История операций";

    public async Task Execute()
    {
        ArgumentNullException.ThrowIfNull(_currentUserService.CurrentAccountNumber);

        ResultTypeWrapper<IList<Transaction>> result = await _accountService.GetTransactionHistory(_currentUserService.CurrentAccountNumber.Value).ConfigureAwait(false);

        if (result.IsSuccess)
        {
            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Тип операции");
            table.AddColumn("Сумма");

            foreach (Transaction transaction in ((SuccessResultWrapper<IList<Transaction>>)result).Data)
            {
                table.AddRow(
                    transaction.Id.ToString(),
                    transaction.TypeOperation,
                    transaction.Amount.ToString());
            }

            AnsiConsole.Write(table);
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Ошибка: {((NotFoundWrapper<IList<Transaction>>)result).ErrorMessage}[/]");
        }
    }
}