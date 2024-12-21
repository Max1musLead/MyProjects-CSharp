using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab3.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models;

public class User : IUser
{
    public Guid Id { get; }

    public string Name { get; }

    private readonly List<UserMessage> _messages;

    public User(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        _messages = new List<UserMessage>();
    }

    public void ReceiveMessage(IMessage message)
    {
        _messages.Add(new UserMessage(message));
    }

    public ResultType MarkAsRead(Guid messageId)
    {
        UserMessage? message = _messages.FirstOrDefault(m => m.Id == messageId);

        if (message == null)
            return new ErrorMessageNotFound("Message not found");

        if (message.Status == MessageStatus.Read)
            return new ErrorMessageAlreadyRead("The message has already been read");

        message.Status = MessageStatus.Read;

        return new SuccessResult();
    }

    public ResultType CheckStatusMessage(Guid idMessage)
    {
        UserMessage? message = _messages.FirstOrDefault(m => m.Id == idMessage);

        if (message == null)
            return new ErrorMessageNotFound("Message not found");

        if (message.Status == MessageStatus.Read)
            return new MessageRead();

        return new MessageUnread();
    }

    private enum MessageStatus
    {
        Unread,
        Read,
    }

    private class UserMessage : IMessage
    {
        public Guid Id { get; }

        public IMessage Message { get; }

        public MessageStatus Status { get; set; }

        public string Title { get; }

        public string Body { get; }

        public int Importance { get; }

        public UserMessage(IMessage message)
        {
            Message = message;
            Id = message.Id;
            Status = MessageStatus.Unread;
            Title = message.Title;
            Body = message.Body;
            Importance = message.Importance;
        }
    }
}