using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Recipients;

public class GroupRecipient : IRecipient
{
    private readonly List<IRecipient> _recipients;

    private GroupRecipient()
    {
        _recipients = new List<IRecipient>();
    }

    public void AddRecipient(IRecipient recipient)
    {
        _recipients.Add(recipient);
    }

    public void ReceiveMessage(IMessage message)
    {
        foreach (IRecipient recipient in _recipients)
        {
            recipient.ReceiveMessage(message);
        }
    }

    public static GroupRecipientBuilder Builder => new GroupRecipientBuilder();

    public class GroupRecipientBuilder : IRecipientBuilder
    {
        private IMessageFilter? _filter;
        private ILogger? _logger;

        public IRecipientBuilder SetFilter(IMessageFilter? filter)
        {
            _filter = filter;
            return this;
        }

        public IRecipientBuilder SetLogger(ILogger? logger)
        {
            _logger = logger;
            return this;
        }

        public IRecipient Build()
        {
            IRecipient recipient = new GroupRecipient();

            if (_logger != null)
            {
                recipient = new LoggerDecorator(recipient, _logger);
            }

            if (_filter != null)
            {
                recipient = new FilterDecorator(recipient, _filter);
            }

            return recipient;
        }
    }
}