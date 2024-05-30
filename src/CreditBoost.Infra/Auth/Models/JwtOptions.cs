namespace CreditBoost.Infra.Auth.Models;

public class JwtOptions
{
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
    public string Secret { get; set; }
}
