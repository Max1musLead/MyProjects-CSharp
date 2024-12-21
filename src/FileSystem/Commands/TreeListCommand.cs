using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class TreeListCommand : ICommand, IEquatable<TreeListCommand>
{
    private readonly IFileSystem _fileSystem;

    private readonly int _depth;

    public TreeListCommand(IFileSystem fileSystem, int depth = 1)
    {
        _fileSystem = fileSystem;
        _depth = depth;
    }

    public void Execute()
    {
        _fileSystem.GetDirectoryTree(_depth);
    }

    public bool Equals(TreeListCommand? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;

        return _depth == other._depth;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;

        return obj.GetType() == GetType() && Equals((TreeListCommand)obj);
    }

    public override int GetHashCode()
    {
        return _depth.GetHashCode();
    }
}