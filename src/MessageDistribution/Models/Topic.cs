using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models;

public class Topic : ITopic
{
    public Guid Id { get; }

    public string TopicName { get; }

    private readonly List<IRecipient> _recipients;

    public Topic(string topicName)
    {
        Id = Guid.NewGuid();
        TopicName = topicName;
        _recipients = new List<IRecipient>();
    }

    public void AddRecipient(IRecipient recipient)
    {
        _recipients.Add(recipient);
    }

    public void SendMessage(Message message)
    {
        foreach (IRecipient recipient in _recipients)
        {
            recipient.ReceiveMessage(message);
        }
    }
}