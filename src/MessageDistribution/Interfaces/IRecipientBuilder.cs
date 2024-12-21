namespace Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

public interface IRecipientBuilder
{
    IRecipientBuilder SetFilter(IMessageFilter? filter);

    IRecipientBuilder SetLogger(ILogger? logger);

    IRecipient Build();
}