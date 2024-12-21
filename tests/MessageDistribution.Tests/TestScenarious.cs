using Itmo.ObjectOrientedProgramming.Lab3.Directors;
using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab3.Models;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Recipients;
using Itmo.ObjectOrientedProgramming.Lab3.ResultTypes;
using Moq;
using Xunit;

namespace Lab3.Tests;

public class TestScenarious
{
    [Fact]
    public void MessageSavedInUnreadStatus()
    {
        // Arrange
        var user = new User("Vasya");
        Message message = CreateMessage();

        // Act
        user.ReceiveMessage(message);
        ResultType result = user.CheckStatusMessage(message.Id);

        // Assert
        Assert.IsType<MessageUnread>(result);
    }

    [Fact]
    public void MarkAsReadUnreadMessage()
    {
        // Arrange
        var user = new User("Vasya");
        Message message = CreateMessage();
        user.ReceiveMessage(message);

        // Act
        ResultType markResult = user.MarkAsRead(message.Id);
        ResultType statusResult = user.CheckStatusMessage(message.Id);

        // Assert
        Assert.IsType<SuccessResult>(markResult);
        Assert.IsType<MessageRead>(statusResult);
    }

    [Fact]
    public void MarkAsReadAlreadyReadMessageReturnError()
    {
        // Arrange
        var user = new User("Vasya");
        Message message = CreateMessage();
        user.ReceiveMessage(message);
        user.MarkAsRead(message.Id);

        // Act
        ResultType markResult = user.MarkAsRead(message.Id);

        // Assert
        Assert.IsType<ErrorMessageAlreadyRead>(markResult);
    }

    [Fact]
    public void MessageWithLowImportanceIsFiltered()
    {
        // Arrange
        var mockFilter = new Mock<IMessageFilter>();
        mockFilter.Setup(f => f.Filter(It.IsAny<IMessage>())).Returns(false);

        var mockRecipient = new Mock<IRecipient>();
        var recipient = new FilterDecorator(mockRecipient.Object, mockFilter.Object);
        Message message = CreateMessage();

        // Act
        recipient.ReceiveMessage(message);

        // Assert
        mockRecipient.Verify(r => r.ReceiveMessage(It.IsAny<IMessage>()), Times.Never);
    }

    [Fact]
    public void ReceiveMessageLog()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();
        var mockRecipient = new Mock<IRecipient>();
        var recipient = new LoggerDecorator(mockRecipient.Object, mockLogger.Object);
        Message message = CreateMessage();

        // Act
        recipient.ReceiveMessage(message);

        // Assert
        mockLogger.Verify(logger => logger.Log(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void SendMessageToMessengerInvokeSend()
    {
        // Arrange
        var mockMessenger = new Mock<IMessenger>();
        IRecipient messengerRecipient = new MessengerRecipient.MessengerRecipientBuilder()
            .SetMessenger(mockMessenger.Object)
            .Build();
        Message message = CreateMessage();

        // Act
        messengerRecipient.ReceiveMessage(message);

        // Assert
        mockMessenger.Verify(m => m.Send(message.Body), Times.Once);
    }

    [Fact]
    public void OnlyOneRecipientReceiveMessage()
    {
        // Arrange
        var user = new User("Vasya");
        var director = new RecipientDirector();

        var mockFilter = new Mock<IMessageFilter>();
        mockFilter.Setup(f => f.Filter(It.Is<IMessage>(m => m.Importance >= 5))).Returns(true);
        var mockLogger = new Mock<ILogger>();

        IRecipient recipient1 =
            director.ConstructUserRecipient(new UserRecipient.UserRecipientBuilder(), user, mockLogger.Object, mockFilter.Object);
        IRecipient recipient2 =
            director.ConstructUserRecipient(new UserRecipient.UserRecipientBuilder(), user, mockLogger.Object);

        var topic = new Topic("TopicName");
        topic.AddRecipient(recipient1);
        topic.AddRecipient(recipient2);

        Message message = CreateMessage(3);

        // Act
        topic.SendMessage(message);

        // Assert
        ResultType userStatus = user.CheckStatusMessage(message.Id);
        Assert.IsType<MessageUnread>(userStatus);

        mockLogger.Verify(logger => logger.Log(It.IsAny<string>()), Times.Once);
    }

    private Message CreateMessage(int importance = 1)
    {
        string title = "Test Title";
        string body = "Test Body";
        return new Message(title, body, importance);
    }
}