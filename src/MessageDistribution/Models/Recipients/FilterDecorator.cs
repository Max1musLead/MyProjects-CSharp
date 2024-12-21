using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Recipients;

public class FilterDecorator : IRecipient
{
    private readonly IRecipient _decoratedRecipient;
    private readonly IMessageFilter _filter;

    public FilterDecorator(IRecipient recipient, IMessageFilter filter)
    {
        _decoratedRecipient = recipient;
        _filter = filter;
    }

    public void ReceiveMessage(IMessage message)
    {
        if (_filter.Filter(message))
        {
            _decoratedRecipient.ReceiveMessage(message);
        }
    }
}