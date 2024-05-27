namespace CreditBoost.Domain.Entities;

public class Beneficiary : Entity
{
    public Guid UserId { get; private set; }
    public string Nickname { get; private set; }
    public decimal Balance { get; private set; }

    // Navigation property
    public User User { get; private set; }
    public ICollection<TopUpTransaction> TopUpTransactions { get; private set; }

    public Beneficiary(Guid id, Guid userId, string nickname, decimal balance)
    {
        Id = id;
        UserId = userId;
        Nickname = nickname;
        Balance = balance;
    }
}
