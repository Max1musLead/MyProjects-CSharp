using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public UserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<Account?> GetAccount(long accountNumber)
    {
        const string sql = """
        SELECT account_number, pin, balance
        FROM accounts
        WHERE account_number = :account_number;
        """;

        using NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("account_number", accountNumber);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        if (await reader.ReadAsync().ConfigureAwait(false))
        {
            return new Account(
                AccountNumber: reader.GetInt64(0),
                Pin: reader.GetString(1),
                Balance: reader.GetDecimal(2));
        }

        return null;
    }

    public async Task CreateAccount(Account account)
    {
        const string sql = """
        INSERT INTO accounts (account_number, pin, balance)
        VALUES (:account_number, :pin, :balance);
        """;

        using NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("account_number", account.AccountNumber)
            .AddParameter("pin", account.Pin)
            .AddParameter("balance", account.Balance);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }

    public async Task UpdateAccount(Account account)
    {
        const string sql = """
        UPDATE accounts
        SET pin = :pin, balance = :balance
        WHERE account_number = :account_number;
        """;

        using NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("account_number", account.AccountNumber)
            .AddParameter("pin", account.Pin)
            .AddParameter("balance", account.Balance);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }
}