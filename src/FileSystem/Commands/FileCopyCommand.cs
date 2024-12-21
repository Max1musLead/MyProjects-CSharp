using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileCopyCommand : ICommand, IEquatable<FileCopyCommand>
{
    private readonly IFileSystem _fileSystem;

    private readonly string _sourcePath;

    private readonly string _destinationPath;

    public FileCopyCommand(IFileSystem fileSystem, string sourcePath, string destinationPath)
    {
        _fileSystem = fileSystem;
        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
    }

    public void Execute()
    {
        _fileSystem.CopyFile(_sourcePath, _destinationPath);
    }

    public bool Equals(FileCopyCommand? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;

        return _sourcePath == other._sourcePath && _destinationPath == other._destinationPath;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;

        return obj.GetType() == GetType() && Equals((FileCopyCommand)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_sourcePath, _destinationPath);
    }
}