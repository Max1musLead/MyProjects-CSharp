namespace Itmo.ObjectOrientedProgramming.Lab4.Visitor.FileSystemComponents;

public interface IFileSystemComponent
{
    void Accept(IFileSystemComponentVisitor visitor);
}