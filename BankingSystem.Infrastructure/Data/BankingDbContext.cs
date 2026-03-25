using Microsoft.EntityFrameworkCore;
using BankingSystem.Domain.Entities;

namespace BankingSystem.Infrastructure.Data;

public class BankingDbContext : DbContext
{
    public BankingDbContext(DbContextOptions<BankingDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
}