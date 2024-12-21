using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

public class DisconnectCommandHandler : CommandHandlerBase
{
    public override ICommand? Handle(IEnumerator<string> request, CommandCurrentFileSystem currentFileSystem)
    {
        if (request.Current is not "disconnect")
            return NextCommand?.Handle(request, currentFileSystem);

        if (currentFileSystem.FileSystem == null)
        {
            return null;
        }

        if (request.MoveNext())
            return null;

        var disconnectCommand = new DisconnectCommand(currentFileSystem.FileSystem);
        currentFileSystem.FileSystem = null;
        return disconnectCommand;
    }
}