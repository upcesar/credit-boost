namespace CreditTopUp.Domain.Entities;

public class TopUpTransaction : Entity
{
    public Guid BeneficiaryId { get; private set; }
    public decimal Amount { get; private set; }
    public decimal Charge { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateTime Timestamp { get; private set; }

    // Navigation properties
    public Beneficiary Beneficiary { get; private set; }

    public TopUpTransaction(
        Guid id,
        Guid beneficiaryId,
        decimal amount,
        decimal charge)
    {
        Id = id;
        BeneficiaryId = beneficiaryId;
        Amount = amount;
        Charge = charge;
        TotalAmount = Amount + Charge;
        Timestamp = DateTime.Now;
    }

    private TopUpTransaction() { }
}
