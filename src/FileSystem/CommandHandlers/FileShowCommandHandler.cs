using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Outputs;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

public class FileShowCommandHandler : CommandHandlerBase
{
    public override ICommand? Handle(IEnumerator<string> request, CommandCurrentFileSystem currentFileSystem)
    {
        if (request.Current is not "file")
            return NextCommand?.Handle(request, currentFileSystem);

        if (!request.MoveNext())
            return null;

        if (request.Current is not "show")
        {
            request.Reset();
            request.MoveNext();
            return NextCommand?.Handle(request, currentFileSystem);
        }

        if (!request.MoveNext())
            return null;

        string filePath = request.Current;

        if (!request.MoveNext() || request.Current is not "-m" || !request.MoveNext())
            return null;

        IOutput? fileOutput = request.Current switch
        {
            "console" => new ConsoleOutput(),
            _ => null,
        };

        if (fileOutput == null || currentFileSystem.FileSystem == null)
            return null;

        return new FileShowCommand(currentFileSystem.FileSystem, fileOutput, filePath);
    }
}