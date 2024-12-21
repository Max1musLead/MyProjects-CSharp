using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

public class TreeListCommandHandler : CommandHandlerBase
{
    public override ICommand? Handle(IEnumerator<string> request, CommandCurrentFileSystem currentFileSystem)
    {
        request.Reset();
        request.MoveNext();

        if (request.Current is not "tree")
            return NextCommand?.Handle(request, currentFileSystem);

        if (!request.MoveNext())
            return null;

        if (request.Current is not "list")
        {
            request.Reset();
            request.MoveNext();
            return NextCommand?.Handle(request, currentFileSystem);
        }

        if (currentFileSystem.FileSystem == null)
            return null;

        if (!request.MoveNext() || request.Current is not "-d" || !request.MoveNext())
            return new TreeListCommand(currentFileSystem.FileSystem);

        int depth = int.Parse(request.Current);

        return new TreeListCommand(currentFileSystem.FileSystem, depth);
    }
}