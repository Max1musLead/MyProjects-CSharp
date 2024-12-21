using Itmo.ObjectOrientedProgramming.Lab4.Outputs;
using Itmo.ObjectOrientedProgramming.Lab4.Visitor.FileSystemComponents;

namespace Itmo.ObjectOrientedProgramming.Lab4.Visitor;

public class ConsoleVisitor : IFileSystemComponentVisitor
{
    private readonly IOutput _output;
    private readonly string _fileSymbol;
    private readonly string _folderSymbol;
    private readonly string _indentation;
    private string _currentIndent = " ";

    public ConsoleVisitor(IOutput output, string fileSymbol = "├─", string folderSymbol = "└─", string indentation = "│  ")
    {
        _output = output;
        _fileSymbol = fileSymbol;
        _folderSymbol = folderSymbol;
        _indentation = indentation;
    }

    public void Visit(FileFileSystemComponent fileComponent)
    {
        _output.Display($"{_currentIndent}{_fileSymbol} {fileComponent.Name}");
    }

    public void Visit(DirectoryFileSystemComponent directoryComponent)
    {
        _output.Display($"{_currentIndent}{_folderSymbol} {directoryComponent.Name}");

        string previousIndent = _currentIndent;
        _currentIndent += _indentation;

        foreach (IFileSystemComponent component in directoryComponent.Components)
        {
            component.Accept(this);
        }

        _currentIndent = previousIndent;
    }
}