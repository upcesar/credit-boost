namespace CreditBoost.Api.Application.Responses;

public sealed class BeneficiaryResponse
{
    public string Nickname { get; set; }
    public decimal Balance { get; set; }
}

public sealed class TopUpOptionResponse
{
    public string Currency => "AED";
    public decimal Amount { get; set; }
}
