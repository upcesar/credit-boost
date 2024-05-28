namespace CreditBoost.Api.Application.Responses;

public sealed class TopUpOptionResponse
{
    public string Currency => "AED";
    public decimal Amount { get; set; }
}
