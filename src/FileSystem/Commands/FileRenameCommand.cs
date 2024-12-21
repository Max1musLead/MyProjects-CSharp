using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileRenameCommand : ICommand, IEquatable<FileRenameCommand>
{
    private readonly IFileSystem _fileSystem;

    private readonly string _path;

    private readonly string _name;

    public FileRenameCommand(IFileSystem fileSystem, string path, string name)
    {
        _fileSystem = fileSystem;
        _path = path;
        _name = name;
    }

    public void Execute()
    {
        _fileSystem.RenameFile(_path, _name);
    }

    public bool Equals(FileRenameCommand? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;

        return _path == other._path && _name == other._name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;

        return obj.GetType() == GetType() && Equals((FileRenameCommand)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_path, _name);
    }
}