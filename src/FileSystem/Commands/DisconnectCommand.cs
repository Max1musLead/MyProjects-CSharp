using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class DisconnectCommand : ICommand, IEquatable<DisconnectCommand>
{
    private readonly IFileSystem _fileSystem;

    public DisconnectCommand(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public void Execute()
    {
        _fileSystem.Disconnect();
    }

    public bool Equals(DisconnectCommand? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;

        return true;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;

        return obj.GetType() == GetType() && Equals((DisconnectCommand)obj);
    }

    public override int GetHashCode()
    {
        return _fileSystem.GetHashCode();
    }
}