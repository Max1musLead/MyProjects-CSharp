using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab5.Aplication.Services;

public class CurrentUserService : ICurrentUserService
{
    public long? CurrentAccountNumber { get; set; }

    public CurrentUserService(long? currentAccountNumber = null)
    {
        CurrentAccountNumber = currentAccountNumber;
    }
}