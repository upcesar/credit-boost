namespace CreditTopUp.Application.Responses;

public class AuthenticationResponse
{
    public bool IsAuthenticated { get; set; }
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}
