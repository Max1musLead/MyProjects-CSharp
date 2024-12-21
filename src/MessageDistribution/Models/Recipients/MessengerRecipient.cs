using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Recipients;

public class MessengerRecipient : IRecipient
{
    private readonly IMessenger _messenger;

    private MessengerRecipient(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public void ReceiveMessage(IMessage message)
    {
        _messenger.Send(message.Body);
    }

    public static MessengerRecipientBuilder Builder => new MessengerRecipientBuilder();

    public class MessengerRecipientBuilder : IRecipientBuilder
    {
        private IMessenger? _messenger;
        private IMessageFilter? _filter;
        private ILogger? _logger;

        public IRecipientBuilder SetMessenger(IMessenger messenger)
        {
            _messenger = messenger;
            return this;
        }

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
            ArgumentNullException.ThrowIfNull(_messenger);

            IRecipient recipient = new MessengerRecipient(_messenger);

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