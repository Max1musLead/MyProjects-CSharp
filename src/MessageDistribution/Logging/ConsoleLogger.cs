using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Logging;

public class ConsoleLogger : ILogger
{
    private static readonly Lazy<ConsoleLogger> Lazy;

    public static ConsoleLogger Instance => Lazy.Value;

    static ConsoleLogger()
    {
        Lazy = new Lazy<ConsoleLogger>(() => new ConsoleLogger());
    }

    private ConsoleLogger() { }

    public void Log(string message)
    {
        Console.WriteLine($"[LOG]: {message}");
    }
}