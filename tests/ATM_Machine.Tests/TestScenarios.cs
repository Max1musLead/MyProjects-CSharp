using Itmo.ObjectOrientedProgramming.Lab5.Aplication.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab5.Aplication.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;
using NSubstitute;
using Xunit;

namespace Lab5.Tests;

public class TestScenarios
{
    private readonly IUserRepository _userRepositoryMock;

    private readonly ITransactionRepository _transactionRepositoryMock;

    private readonly AccountService _accountService;

    public TestScenarios()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _transactionRepositoryMock = Substitute.For<ITransactionRepository>();
        _accountService = new AccountService(_userRepositoryMock, _transactionRepositoryMock);
    }

    [Fact]
    public async Task WithdrawSuccess()
    {
        // Arrange
        const long accountNumber = 12345678;
        const decimal initialBalance = 1000;
        const decimal withdrawAmount = 200;
        const decimal expectedBalance = initialBalance - withdrawAmount;

        var account = new Account(accountNumber, "pin", initialBalance);
        _userRepositoryMock.GetAccount(accountNumber).Returns(account);

        // Act
        ResultType result = await _accountService.WithdrawMoney(accountNumber, withdrawAmount);

        // Assert
        Assert.IsType<SuccessResult>(result);

        await _userRepositoryMock.Received(1).UpdateAccount(
            Arg.Is<Account>(a =>
            a.AccountNumber == accountNumber &&
            a.Balance == expectedBalance));

        await _transactionRepositoryMock.Received(1).AddTransaction(
            Arg.Is<Transaction>(t =>
            t.AccountNumber == accountNumber &&
            t.Amount == withdrawAmount &&
            t.TypeOperation == "Списание"));
    }

    [Fact]
    public async Task WithdrawError()
    {
        // Arrange
        const long accountNumber = 12345678;
        const decimal initialBalance = 100;
        const decimal withdrawAmount = 200;

        var account = new Account(accountNumber, "pin", initialBalance);
        _userRepositoryMock.GetAccount(accountNumber).Returns(account);

        // Act
        ResultType result = await _accountService.WithdrawMoney(accountNumber, withdrawAmount);

        // Assert
        Assert.IsType<InsufficientFunds>(result);

        await _userRepositoryMock.DidNotReceive().UpdateAccount(Arg.Any<Account>());

        await _transactionRepositoryMock.DidNotReceive().AddTransaction(Arg.Any<Transaction>());
    }

    [Fact]
    public async Task DepositMoneySuccess()
    {
        // Arrange
        const long accountNumber = 12345678;
        const decimal initialBalance = 1000;
        const decimal depositAmount = 200;
        const decimal expectedBalance = initialBalance + depositAmount;

        var account = new Account(accountNumber, "pin", initialBalance);

        _userRepositoryMock.GetAccount(accountNumber).Returns(account);

        // Act
        ResultType result = await _accountService.DepositMoney(accountNumber, depositAmount);

        // Assert
        Assert.IsType<SuccessResult>(result);

        await _userRepositoryMock.Received(1).UpdateAccount(
            Arg.Is<Account>(a =>
            a.AccountNumber == accountNumber &&
            a.Balance == expectedBalance));

        await _transactionRepositoryMock.Received(1).AddTransaction(
            Arg.Is<Transaction>(t =>
            t.AccountNumber == accountNumber &&
            t.Amount == depositAmount &&
            t.TypeOperation == "Пополнение"));
    }
}