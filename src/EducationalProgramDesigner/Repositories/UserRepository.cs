using Itmo.ObjectOrientedProgramming.Lab2.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class UserRepository
{
    private static readonly Lazy<UserRepository> Lazy;

    public static UserRepository Instance => Lazy.Value;

    private readonly Dictionary<Guid, User> _users;

    static UserRepository()
    {
        Lazy = new Lazy<UserRepository>(() => new UserRepository());
    }

    private UserRepository()
    {
        _users = new Dictionary<Guid, User>();
    }

    public void Add(User user)
    {
        _users[user.UserId] = user;
    }

    public User? GetById(Guid userId)
    {
        _users.TryGetValue(userId, out User? user);
        return user;
    }
}