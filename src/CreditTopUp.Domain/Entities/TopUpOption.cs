namespace CreditTopUp.Domain.Entities;

public class TopUpOption : Entity
{
    public decimal Amount { get; private set; }

    public TopUpOption(Guid id, decimal amount)
    {
        Id = id;
        Amount = amount;
    }

    private TopUpOption() { }
}
