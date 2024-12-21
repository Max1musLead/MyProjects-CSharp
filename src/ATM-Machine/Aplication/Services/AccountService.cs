using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypeWrappers;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

namespace Itmo.ObjectOrientedProgramming.Lab5.Aplication.Services;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;

    private readonly ITransactionRepository _transactionRepository;

    public AccountService(IUserRepository userRepository, ITransactionRepository transactionRepository)
    {
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<ResultType> CreateAccount(long accountNumber, string pin)
    {
        if (accountNumber < 10000000)
            return new ErrorResult("Счёт должен состоять минимум из 8 символов.");
        if (pin.Length < 4)
            return new ErrorResult("Pin должен состоять минимум из 4 символов.");

        Account? tempAccount = await _userRepository.GetAccount(accountNumber).ConfigureAwait(false);

        if (tempAccount != null)
            return new ErrorResult("Счёт с таким номером уже существует.");

        var newAccount = new Account(accountNumber, BCrypt.Net.BCrypt.HashPassword(pin), 0);
        await _userRepository.CreateAccount(newAccount).ConfigureAwait(false);

        return new SuccessResult();
    }

    public async Task<ResultTypeWrapper<decimal>> GetAccountBalance(long accountNumber)
    {
        Account? tempAccount = await _userRepository.GetAccount(accountNumber).ConfigureAwait(false);

        if (tempAccount == null)
            return new NotFoundWrapper<decimal>("Счёт не найден.");

        return new SuccessResultWrapper<decimal>(tempAccount.Balance);
    }

    public async Task<ResultType> WithdrawMoney(long accountNumber, decimal amount)
    {
        Account? tempAccount = await _userRepository.GetAccount(accountNumber).ConfigureAwait(false);
        if (tempAccount == null)
            return new NotFound("Счёт не найден.");

        if (tempAccount.Balance < amount)
            return new InsufficientFunds("Недостаточно средств на счёте.");

        tempAccount = tempAccount with { Balance = tempAccount.Balance - amount };
        await _userRepository.UpdateAccount(tempAccount).ConfigureAwait(false);

        var newTransaction = new Transaction(
            Guid.NewGuid(),
            accountNumber,
            amount,
            "Списание");
        await _transactionRepository.AddTransaction(newTransaction).ConfigureAwait(false);

        return new SuccessResult();
    }

    public async Task<ResultType> DepositMoney(long accountNumber, decimal amount)
    {
        Account? tempAccount = await _userRepository.GetAccount(accountNumber).ConfigureAwait(false);
        if (tempAccount == null)
            return new NotFound("Счёт не найден.");

        tempAccount = tempAccount with { Balance = tempAccount.Balance + amount };
        await _userRepository.UpdateAccount(tempAccount).ConfigureAwait(false);

        var newTransaction = new Transaction(
            Guid.NewGuid(),
            accountNumber,
            amount,
            "Пополнение");
        await _transactionRepository.AddTransaction(newTransaction).ConfigureAwait(false);

        return new SuccessResult();
    }

    public async Task<ResultTypeWrapper<IList<Transaction>>> GetTransactionHistory(long accountNumber)
    {
        Account? tempAccount = await _userRepository.GetAccount(accountNumber).ConfigureAwait(false);
        if (tempAccount == null)
            return new NotFoundWrapper<IList<Transaction>>("Счёт не найден.");

        IList<Transaction> transactions =
            await _transactionRepository.GetTransactions(accountNumber).ConfigureAwait(false);

        return new SuccessResultWrapper<IList<Transaction>>(transactions);
    }
}