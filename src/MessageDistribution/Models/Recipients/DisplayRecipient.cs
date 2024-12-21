using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;
using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Recipients;

public class DisplayRecipient : IRecipient
{
    private readonly IDisplay _display;
    private readonly Color _color;

    private DisplayRecipient(IDisplay display, Color color)
    {
        _display = display;
        _color = color;
    }

    public void ReceiveMessage(IMessage message)
    {
        _display.DisplayOnscreen(message.Body, _color);
    }

    public static DisplayRecipientBuilder Builder => new DisplayRecipientBuilder();

    public class DisplayRecipientBuilder : IRecipientBuilderWithDisplay
    {
        private Color _color;
        private IDisplay? _display;
        private ILogger? _logger;
        private IMessageFilter? _filter;

        public IRecipientBuilderWithDisplay SetColor(Color color)
        {
            _color = color;
            return this;
        }

        public IRecipientBuilderWithDisplay SetDisplay(IDisplay display)
        {
            _display = display;
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
            ArgumentNullException.ThrowIfNull(_display);

            IRecipient recipient = new DisplayRecipient(_display, _color);

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