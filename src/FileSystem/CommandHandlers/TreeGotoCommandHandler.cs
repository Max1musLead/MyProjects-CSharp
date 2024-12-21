using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

public class TreeGotoCommandHandler : CommandHandlerBase
{
    public override ICommand? Handle(IEnumerator<string> request, CommandCurrentFileSystem currentFileSystem)
    {
        if (request.Current is not "tree")
            return NextCommand?.Handle(request, currentFileSystem);

        if (!request.MoveNext())
            return null;

        if (request.Current is not "goto")
        {
            request.Reset();
            request.MoveNext();
            return NextCommand?.Handle(request, currentFileSystem);
        }

        if (!request.MoveNext())
            return null;

        string path = request.Current;

        if (currentFileSystem.FileSystem == null)
            return null;

        return new TreeGotoCommand(currentFileSystem.FileSystem, path);
    }
}