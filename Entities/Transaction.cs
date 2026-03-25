namespace BankingSystem.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }

    public Guid FromAccountId { get; set; }
    public Guid ToAccountId { get; set; }

    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}