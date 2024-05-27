namespace CreditBoost.Domain.Entities;

public class User : Entity
{
    public string UserName { get; private set; }
    public string IdentityUserId { get; private set; }
    public bool IsVerified { get; private set; }

    // Navigation properties
    public ICollection<Beneficiary> Beneficiaries { get; private set; }
    public ICollection<TopUpTransaction> TopUpTransactions { get; private set; }

    public User(Guid id, string identityUserId, string username)
    {
        Id = id;
        IdentityUserId = identityUserId;
        UserName = username;
    }

    private User() { }

    public void Verify()
    {
        IsVerified = true;
    }
}
