using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.DataAccess.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public TransactionRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task AddTransaction(Transaction transaction)
    {
        const string sql = """
        INSERT INTO transactions (id, account_number, amount, type_operation)
        VALUES (:id, :account_number, :amount, :type_operation);
        """;

        using NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", transaction.Id)
            .AddParameter("account_number", transaction.AccountNumber)
            .AddParameter("amount", transaction.Amount)
            .AddParameter("type_operation", transaction.TypeOperation);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }

    public async Task<IList<Transaction>> GetTransactions(long accountNumber)
    {
        const string sql = """
        SELECT id, account_number, amount, type_operation
        FROM transactions
        WHERE account_number = :account_number;
        """;

        using NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("account_number", accountNumber);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        var transactions = new List<Transaction>();

        while (await reader.ReadAsync().ConfigureAwait(false))
        {
            var transaction = new Transaction(
                Id: reader.GetGuid(0),
                AccountNumber: reader.GetInt64(1),
                Amount: reader.GetDecimal(2),
                TypeOperation: reader.GetString(3));

            transactions.Add(transaction);
        }

        return transactions;
    }
}