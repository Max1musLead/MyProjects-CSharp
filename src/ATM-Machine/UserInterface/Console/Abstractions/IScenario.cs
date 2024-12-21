namespace Itmo.ObjectOrientedProgramming.Lab5.UserInterface.Console.Abstractions;

public interface IScenario
{
    string Name { get; }

    void Run();
}