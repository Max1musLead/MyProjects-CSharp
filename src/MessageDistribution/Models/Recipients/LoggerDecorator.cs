using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Recipients;

public class LoggerDecorator : IRecipient
{
    private readonly IRecipient _decoratedRecipient;
    private readonly ILogger _logger;

    public LoggerDecorator(IRecipient recipient, ILogger logger)
    {
        _decoratedRecipient = recipient;
        _logger = logger;
    }

    public void ReceiveMessage(IMessage message)
    {
        _logger.Log($"Message '{message.Title}' received");
        _decoratedRecipient.ReceiveMessage(message);
    }
}