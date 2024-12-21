namespace Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

public record Transaction(Guid Id, long AccountNumber, decimal Amount, string TypeOperation);