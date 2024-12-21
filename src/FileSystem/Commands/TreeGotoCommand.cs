using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class TreeGotoCommand : ICommand, IEquatable<TreeGotoCommand>
{
    private readonly IFileSystem _fileSystem;

    private readonly string _path;

    public TreeGotoCommand(IFileSystem fileSystem, string path)
    {
        _fileSystem = fileSystem;
        _path = path;
    }

    public void Execute()
    {
        _fileSystem.ChangeDirectory(_path);
    }

    public bool Equals(TreeGotoCommand? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;

        return _path == other._path;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;

        return obj.GetType() == GetType() && Equals((TreeGotoCommand)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_path);
    }
}