namespace Itmo.ObjectOrientedProgramming.Lab4.Outputs;

public class ConsoleOutput : IOutput
{
    public void Display(string message)
    {
        Console.WriteLine(message);
    }
}