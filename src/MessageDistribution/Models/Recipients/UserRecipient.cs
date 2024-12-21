using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Recipients;

public class UserRecipient : IRecipient
{
    private readonly IUser _user;

    private UserRecipient(IUser user)
    {
        _user = user;
    }

    public void ReceiveMessage(IMessage message)
    {
        _user.ReceiveMessage(message);
    }

    public static UserRecipientBuilder Builder => new UserRecipientBuilder();

    public class UserRecipientBuilder : IRecipientBuilderWithUser
    {
        private IUser? _user;
        private IMessageFilter? _filter;
        private ILogger? _logger;

        public IRecipientBuilder SetUser(IUser user)
        {
            _user = user;
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
            ArgumentNullException.ThrowIfNull(_user);

            IRecipient recipient = new UserRecipient(_user);

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