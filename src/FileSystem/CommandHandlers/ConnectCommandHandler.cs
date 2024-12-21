using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

public class ConnectCommandHandler : CommandHandlerBase
{
    public override ICommand? Handle(IEnumerator<string> request, CommandCurrentFileSystem currentFileSystem)
    {
        if (request.Current is not "connect")
            return NextCommand?.Handle(request, currentFileSystem);

        if (!request.MoveNext())
            return null;

        string address = request.Current;

        if (!request.MoveNext() || request.Current is not "-m" || !request.MoveNext())
            return null;

        IFileSystem? fileSystem = request.Current switch
        {
            "local" => new LocalFileSystem(),
            _ => null,
        };

        if (fileSystem == null)
            return null;

        currentFileSystem.FileSystem = fileSystem;
        return new ConnectCommand(currentFileSystem.FileSystem, address);
    }
}