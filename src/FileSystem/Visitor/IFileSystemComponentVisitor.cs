using Itmo.ObjectOrientedProgramming.Lab4.Visitor.FileSystemComponents;

namespace Itmo.ObjectOrientedProgramming.Lab4.Visitor;

public interface IFileSystemComponentVisitor
{
    void Visit(FileFileSystemComponent fileComponent);

    void Visit(DirectoryFileSystemComponent directoryComponent);
}