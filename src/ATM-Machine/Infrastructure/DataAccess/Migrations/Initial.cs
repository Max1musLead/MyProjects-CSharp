using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        """
        CREATE TABLE IF NOT EXISTS accounts (
            account_number BIGINT PRIMARY KEY,
            pin TEXT NOT NULL,
            balance DECIMAL NOT NULL DEFAULT 0
        );

        CREATE TABLE IF NOT EXISTS transactions (
            id UUID PRIMARY KEY,
            account_number BIGINT NOT NULL REFERENCES accounts(account_number),
            amount DECIMAL NOT NULL,
            type_operation TEXT NOT NULL
        );
        """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
        DROP TABLE IF EXISTS transactions;
        DROP TABLE IF EXISTS accounts;
        """;
}