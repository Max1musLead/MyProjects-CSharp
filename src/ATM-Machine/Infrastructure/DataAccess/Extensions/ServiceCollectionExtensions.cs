using Itmo.Dev.Platform.Common.Extensions;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(this IServiceCollection services, Action<PostgresConnectionConfiguration> configure)
    {
        services.AddPlatform();
        services.AddPlatformPostgres(builder => builder.Configure(configure));
        services.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}