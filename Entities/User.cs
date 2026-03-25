namespace BankingSystem.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public List<Account> Accounts { get; set; } = new();
}