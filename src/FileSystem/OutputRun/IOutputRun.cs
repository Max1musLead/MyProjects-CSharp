using Itmo.ObjectOrientedProgramming.Lab4.CommandHandlers;

namespace Itmo.ObjectOrientedProgramming.Lab4.OutputRun;

public interface IOutputRun
{
    void Run(IEnumerable<string> args, CommandCurrentFileSystem currentFileSystem);
}