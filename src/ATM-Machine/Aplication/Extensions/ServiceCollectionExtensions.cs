using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Aplication.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<ICurrentUserService, CurrentUserService>();
        collection.AddScoped<IAccountService, AccountService>();
        collection.AddScoped<IUserService, UserService>(sp =>
        {
            IUserRepository account = sp.GetRequiredService<IUserRepository>();
            return new UserService(account, "admin123");
        });

        return collection;
    }
}