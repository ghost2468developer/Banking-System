using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entities;
using BankingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Application.Services;

public class AccountService : IAccountService
{
    private readonly BankingDbContext _context;

    public AccountService(BankingDbContext context)
    {
        _context = context;
    }

    public async Task<Account> CreateAccount(Guid userId)
    {
        var account = new Account
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            AccountNumber = GenerateAccountNumber(),
            Balance = 0
        };

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        return account;
    }

    public async Task Deposit(Guid accountId, decimal amount)
    {
        if (amount <= 0)
            throw new Exception("Amount must be greater than zero");

        var account = await _context.Accounts.FindAsync(accountId);

        if (account == null)
            throw new Exception("Account not found");

        account.Balance += amount;

        await _context.SaveChangesAsync();
    }

    public async Task Withdraw(Guid accountId, decimal amount)
    {
        if (amount <= 0)
            throw new Exception("Amount must be greater than zero");

        var account = await _context.Accounts.FindAsync(accountId);

        if (account == null)
            throw new Exception("Account not found");

        if (account.Balance < amount)
            throw new Exception("Insufficient funds");

        account.Balance -= amount;

        await _context.SaveChangesAsync();
    }

    public async Task Transfer(Guid fromAccountId, Guid toAccountId, decimal amount)
    {
        if (fromAccountId == toAccountId)
            throw new Exception("Cannot transfer to the same account");

        var fromAccount = await _context.Accounts.FindAsync(fromAccountId);
        var toAccount = await _context.Accounts.FindAsync(toAccountId);

        if (fromAccount == null || toAccount == null)
            throw new Exception("Account not found");

        if (fromAccount.Balance < amount)
            throw new Exception("Insufficient funds");

        fromAccount.Balance -= amount;
        toAccount.Balance += amount;

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            FromAccountId = fromAccountId,
            ToAccountId = toAccountId,
            Amount = amount,
            CreatedAt = DateTime.UtcNow
        };

        _context.Transactions.Add(transaction);

        await _context.SaveChangesAsync();
    }

    private string GenerateAccountNumber()
    {
        return $"ACC-{DateTime.UtcNow.Ticks}";
    }
}