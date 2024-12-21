using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

namespace Itmo.ObjectOrientedProgramming.Lab5.Domain.Abstractions;

public interface ITransactionRepository
{
    Task AddTransaction(Transaction transaction);

    Task<IList<Transaction>> GetTransactions(long accountNumber);
}