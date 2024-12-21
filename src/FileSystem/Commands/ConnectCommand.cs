using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class ConnectCommand : ICommand, IEquatable<ConnectCommand>
{
    private readonly IFileSystem _fileSystem;

    private readonly string _address;

    public ConnectCommand(IFileSystem fileSystem, string address)
    {
        _fileSystem = fileSystem;
        _address = address;
    }

    public void Execute()
    {
        _fileSystem.Connect(_address);
    }

    public bool Equals(ConnectCommand? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;

        return _address == other._address;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;

        return obj.GetType() == GetType() && Equals((ConnectCommand)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_address);
    }
}