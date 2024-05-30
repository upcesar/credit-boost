namespace CreditTopUp.Domain.Entities;

public class User : Entity
{
    public string UserName { get; private set; }
    public bool IsVerified { get; private set; }

    // Navigation properties
    public ICollection<Beneficiary> Beneficiaries { get; private set; }

    public User(Guid id, string username)
    {
        Id = id;
        UserName = username;
    }

    private User() { }

    public void Verify()
    {
        IsVerified = true;
    }
}
