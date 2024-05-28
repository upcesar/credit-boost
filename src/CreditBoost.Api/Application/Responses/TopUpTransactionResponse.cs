namespace CreditBoost.Api.Application.Responses;

public class TopUpTransactionResponse
{
    public Guid BeneficiaryId { get; set; }
    public decimal Amount { get; set; }
    public decimal Charge { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime Timestamp { get; set; }
}
