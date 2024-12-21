using Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;
using Xunit;

namespace Lab4.Tests;

public class TestScenarious
{
    private readonly IFileSystem _fileSystem = new LocalFileSystem();

    [Theory]
    [ClassData(typeof(CommandTestDataCorrect))]
    public void CommandTestsCorrect(IEnumerable<string> args, ICommand? expectedCommand)
    {
        // Arrange
        ICommandHandler commandHandlers = new ConnectCommandHandler()
            .AddNext(new DisconnectCommandHandler())
            .AddNext(new FileCopyCommandHandler())
            .AddNext(new FileDeleteCommandHandler())
            .AddNext(new FileMoveCommandHandler())
            .AddNext(new FileRenameCommandHandler())
            .AddNext(new FileShowCommandHandler())
            .AddNext(new TreeGotoCommandHandler())
            .AddNext(new TreeListCommandHandler());

        var currentFileSystem = new CommandCurrentFileSystem
        {
            FileSystem = _fileSystem,
        };

        // Act
        using IEnumerator<string> request = args.GetEnumerator();

        if (!request.MoveNext())
        {
            Assert.False(true);
        }

        ICommand? command = commandHandlers.Handle(request, currentFileSystem);

        // Assert
        Assert.NotNull(command);
        Assert.True(command.Equals(expectedCommand));
    }

    [Theory]
    [ClassData(typeof(CommandTestDataIncorrect))]
    public void CommandTestsIncorrect(IEnumerable<string> args)
    {
        // Arrange
        ICommandHandler commandHandlers = new ConnectCommandHandler()
            .AddNext(new DisconnectCommandHandler())
            .AddNext(new FileCopyCommandHandler())
            .AddNext(new FileDeleteCommandHandler())
            .AddNext(new FileMoveCommandHandler())
            .AddNext(new FileRenameCommandHandler())
            .AddNext(new FileShowCommandHandler())
            .AddNext(new TreeGotoCommandHandler())
            .AddNext(new TreeListCommandHandler());

        var currentFileSystem = new CommandCurrentFileSystem
        {
            FileSystem = _fileSystem,
        };

        // Act
        using IEnumerator<string> request = args.GetEnumerator();

        if (!request.MoveNext())
        {
            Assert.False(true);
        }

        ICommand? command = commandHandlers.Handle(request, currentFileSystem);

        // Assert
        Assert.Null(command);
    }
}