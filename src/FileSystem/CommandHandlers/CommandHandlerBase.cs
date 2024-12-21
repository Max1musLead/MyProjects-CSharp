using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

public abstract class CommandHandlerBase : ICommandHandler
{
    protected ICommandHandler? NextCommand { get; private set; }

    public ICommandHandler AddNext(ICommandHandler handler)
    {
        if (NextCommand != null)
        {
            NextCommand.AddNext(handler);
        }
        else
        {
            NextCommand = handler;
        }

        return this;
    }

    public abstract ICommand? Handle(IEnumerator<string> request, CommandCurrentFileSystem currentFileSystem);
}