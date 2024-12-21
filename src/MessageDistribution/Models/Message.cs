using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models;

public class Message : IMessage
{
    public Guid Id { get; }

    public string Title { get; }

    public string Body { get; }

    public int Importance { get; }

    public Message(string title, string body, int importance)
    {
        Id = Guid.NewGuid();
        Title = title;
        Body = body;
        Importance = importance;
    }
}