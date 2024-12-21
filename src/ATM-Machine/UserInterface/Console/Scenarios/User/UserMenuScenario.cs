using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Commands;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Scenarios.User;

public class UserMenuScenario : IScenario
{
    private readonly IList<ICommand> _commands;

    public UserMenuScenario(ICurrentUserService currentUserService, IAccountService accountService)
    {
        _commands = new List<ICommand>()
        {
            new ViewBalanceCommand(accountService, currentUserService),
            new WithdrawMoneyCommand(accountService, currentUserService),
            new DepositMoneyCommand(accountService, currentUserService),
            new ViewTransactionHistoryCommand(accountService, currentUserService),
        };
    }

    public string Name => "Меню пользователя";

    public void Run()
    {
        while (true)
        {
            string[] choices = _commands.Select(c => c.Name).Append("Назад").ToArray();
            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Выберите действие:")
                    .AddChoices(choices));

            if (choice == "Назад")
                break;

            ICommand command = _commands.First(c => c.Name == choice);
            command.Execute().GetAwaiter().GetResult();
        }
    }
}