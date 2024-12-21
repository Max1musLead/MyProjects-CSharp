﻿using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

public class FileMoveCommandHandler : CommandHandlerBase
{
    public override ICommand? Handle(IEnumerator<string> request, CommandCurrentFileSystem currentFileSystem)
    {
        if (request.Current is not "file")
            return NextCommand?.Handle(request, currentFileSystem);

        if (!request.MoveNext())
            return null;

        if (request.Current is not "move")
        {
            request.Reset();
            request.MoveNext();
            return NextCommand?.Handle(request, currentFileSystem);
        }

        if (!request.MoveNext())
            return null;

        string sourcePath = request.Current;

        if (!request.MoveNext())
            return null;

        string destinationPath = request.Current;

        if (currentFileSystem.FileSystem == null)
            return null;

        return new FileMoveCommand(currentFileSystem.FileSystem, sourcePath, destinationPath);
    }
}