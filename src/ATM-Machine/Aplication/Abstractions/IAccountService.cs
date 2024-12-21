using Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypeWrappers;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

namespace Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;

public interface IAccountService
{
    Task<ResultType> CreateAccount(long accountNumber, string pin);

    Task<ResultTypeWrapper<decimal>> GetAccountBalance(long accountNumber);

    Task<ResultType> WithdrawMoney(long accountNumber, decimal amount);

    Task<ResultType> DepositMoney(long accountNumber, decimal amount);

    Task<ResultTypeWrapper<IList<Transaction>>> GetTransactionHistory(long accountNumber);
}