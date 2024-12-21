using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

namespace Itmo.ObjectOrientedProgramming.Lab5.Aplication.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    private readonly string _adminPassword;

    public UserService(IUserRepository userRepository, string adminPassword)
    {
        _userRepository = userRepository;
        _adminPassword = adminPassword;
    }

    public async Task<ResultType> AuthenticateUser(long accountNumber, string pin)
    {
        Account? tempAccount = await _userRepository.GetAccount(accountNumber).ConfigureAwait(false);

        if (tempAccount == null || !BCrypt.Net.BCrypt.Verify(pin, tempAccount.Pin))
            return new IncorrectPin("Неверный счёт или пин.");

        return new SuccessResult();
    }

    public bool AuthenticateAdmin(string password)
    {
        return password == _adminPassword;
    }
}