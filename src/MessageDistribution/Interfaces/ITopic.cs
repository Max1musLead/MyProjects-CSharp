using Itmo.ObjectOrientedProgramming.Lab3.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

public interface ITopic
{
    Guid Id { get; }

    string TopicName { get; }

    void AddRecipient(IRecipient recipient);

    void SendMessage(Message message);
}