using BankingSystem.Domain.Entities;

namespace BankingSystem.Application.Interfaces;

public interface IAccountService
{
    Task<Account> CreateAccount(Guid userId);
    Task Deposit(Guid accountId, decimal amount);
    Task Withdraw(Guid accountId, decimal amount);
    Task Transfer(Guid fromAccountId, Guid toAccountId, decimal amount);
}