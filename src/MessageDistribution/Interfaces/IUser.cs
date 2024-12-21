using Itmo.ObjectOrientedProgramming.Lab3.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

public interface IUser
{
    Guid Id { get; }

    string Name { get; }

    void ReceiveMessage(IMessage message);

    ResultType MarkAsRead(Guid messageId);
}