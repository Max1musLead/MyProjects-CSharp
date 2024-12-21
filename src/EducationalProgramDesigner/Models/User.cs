namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public record User
{
    public Guid UserId { get; }

    public string Name { get; }

    public User(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        UserId = Guid.NewGuid();
        Name = name;
    }
}