namespace Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

public interface IMessage
{
    Guid Id { get; }

    string Title { get; }

    string Body { get; }

    int Importance { get; }
}