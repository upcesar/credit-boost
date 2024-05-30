namespace CreditTopUp.Domain.Events;

public class TopUpTransactionCreated
{
    public Guid TransactionId { get; set; }
    public Guid BeneficiaryId { get; set; }
    public decimal Amount { get; set; }
}
