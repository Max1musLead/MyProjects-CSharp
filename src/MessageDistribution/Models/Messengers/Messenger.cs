using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Messengers;

public class Messenger : IMessenger
{
    public Guid Id { get; }

    public Messenger()
    {
        Id = Guid.NewGuid();
    }

    public void Send(string message)
    {
        Console.WriteLine($"Messenger: {message}");
    }
}