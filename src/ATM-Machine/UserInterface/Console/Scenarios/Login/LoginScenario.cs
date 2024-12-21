using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Commands;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Scenarios.Login;

public class LoginScenario : IScenario
{
    private readonly IUserService _userService;

    private readonly ICurrentUserService _currentUserService;

    public LoginScenario(IUserService userService, ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }

    public string Name => "Вход";

    public void Run()
    {
        string role = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Выберите режим:")
                .AddChoices("Пользователь", "Администратор"));

        switch (role)
        {
            case "Пользователь":
            {
                var command = new UserLoginCommand(_userService, _currentUserService);
                command.Execute().GetAwaiter().GetResult();
                break;
            }

            case "Администратор":
            {
                var command = new AdminLoginCommand(_userService, _currentUserService);
                command.Execute();
                break;
            }

            default:
                break;
        }
    }
}