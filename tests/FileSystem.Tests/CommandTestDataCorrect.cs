using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Outputs;
using System.Collections;

namespace Lab4.Tests;

public class CommandTestDataCorrect : IEnumerable<object[]>
{
    private readonly IFileSystem _fileSystem = new LocalFileSystem();
    private readonly IOutput _consoleOutput = new ConsoleOutput();

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new string[] { "connect", "/test/path", "-m", "local" },
            new ConnectCommand(_fileSystem, "/test/path"),
        };

        yield return new object[]
        {
            new string[] { "disconnect" },
            new DisconnectCommand(_fileSystem),
        };

        yield return new object[]
        {
            new string[] { "file", "copy", "/source/test/path", "/destination/test/path" },
            new FileCopyCommand(_fileSystem, "/source/test/path", "/destination/test/path"),
        };

        yield return new object[]
        {
            new string[] { "file", "delete", "/test/path" },
            new FileDeleteCommand(_fileSystem, "/test/path"),
        };

        yield return new object[]
        {
            new string[] { "file", "move", "/source/test/path", "/destination/test/path" },
            new FileMoveCommand(_fileSystem, "/source/test/path", "/destination/test/path"),
        };

        yield return new object[]
        {
            new string[] { "file", "rename", "/test/path", "name.txt" },
            new FileRenameCommand(_fileSystem, "/test/path", "name.txt"),
        };

        yield return new object[]
        {
            new string[] { "file", "show", "/test/path", "-m", "console" },
            new FileShowCommand(_fileSystem, _consoleOutput, "/test/path"),
        };

        yield return new object[]
        {
            new string[] { "tree", "goto", "/test/path" },
            new TreeGotoCommand(_fileSystem, "/test/path"),
        };

        yield return new object[]
        {
            new string[] { "tree", "list", "-d", "2" },
            new TreeListCommand(_fileSystem, 2),
        };

        yield return new object[]
        {
            new string[] { "tree", "list" },
            new TreeListCommand(_fileSystem),
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}