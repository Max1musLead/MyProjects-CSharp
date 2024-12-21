using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

namespace Itmo.ObjectOrientedProgramming.Lab5.Domain.Abstractions;

public interface IUserRepository
{
    Task<Account?> GetAccount(long accountNumber);

    Task CreateAccount(Account account);

    Task UpdateAccount(Account account);
}