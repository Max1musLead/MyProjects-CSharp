using Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.OutputRun;

public class OutputRunner : IOutputRun
{
    private readonly ICommandHandler _commandHandler;

    public OutputRunner(ICommandHandler commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public void Run(IEnumerable<string> args, CommandCurrentFileSystem currentFileSystem)
    {
        using IEnumerator<string> request = args.GetEnumerator();

        while (request.MoveNext())
        {
            ICommand? command = _commandHandler.Handle(request, currentFileSystem);

            if (command is not null)
            {
                command.Execute();
            }
        }
    }
}