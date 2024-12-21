using Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;

public interface IUserService
{
    Task<ResultType> AuthenticateUser(long accountNumber, string pin);

    bool AuthenticateAdmin(string password);
}