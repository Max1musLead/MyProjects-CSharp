using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Outputs;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileShowCommand : ICommand, IEquatable<FileShowCommand>
{
    private readonly IFileSystem _fileSystem;

    private readonly IOutput _output;

    private readonly string _path;

    public FileShowCommand(IFileSystem fileSystem, IOutput output,  string path)
    {
        _fileSystem = fileSystem;
        _output = output;
        _path = path;
    }

    public void Execute()
    {
        _fileSystem.ShowFile(_path, _output);
    }

    public bool Equals(FileShowCommand? other)
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

        return obj.GetType() == GetType() && Equals((FileShowCommand)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_path);
    }
}