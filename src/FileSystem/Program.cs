using Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.OutputRun;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public class Program
{
    public static void Main(string[] args)
    {
        ICommandHandler commandHandlers = new ConnectCommandHandler()
            .AddNext(new DisconnectCommandHandler())
            .AddNext(new FileCopyCommandHandler())
            .AddNext(new FileDeleteCommandHandler())
            .AddNext(new FileMoveCommandHandler())
            .AddNext(new FileRenameCommandHandler())
            .AddNext(new FileShowCommandHandler())
            .AddNext(new TreeGotoCommandHandler())
            .AddNext(new TreeListCommandHandler());

        var runner = new OutputRunner(commandHandlers);

        var currentFileSystem = new CommandCurrentFileSystem();

        while (true)
        {
            Console.Write("> ");

            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            string[] inputArgs = input.Split(' ');

            if (inputArgs.Length == 0)
                continue;

            if (inputArgs[0] == "exit")
                break;

            runner.Run(inputArgs, currentFileSystem);
        }
    }
}