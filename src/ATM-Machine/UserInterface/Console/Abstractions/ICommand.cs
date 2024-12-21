namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;

public interface ICommand
{
    string Name { get; }

    Task Execute();
}