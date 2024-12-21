using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

public interface ICommandHandler
{
    ICommand? Handle(IEnumerator<string> request, CommandCurrentFileSystem currentFileSystem);

    ICommandHandler AddNext(ICommandHandler handler);
}