using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileDeleteCommand : ICommand, IEquatable<FileDeleteCommand>
{
    private readonly IFileSystem _fileSystem;

    private readonly string _path;

    public FileDeleteCommand(IFileSystem fileSystem, string path)
    {
        _fileSystem = fileSystem;
        _path = path;
    }

    public void Execute()
    {
        _fileSystem.DeleteFile(_path);
    }

    public bool Equals(FileDeleteCommand? other)
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

        return obj.GetType() == GetType() && Equals((FileDeleteCommand)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_path);
    }
}