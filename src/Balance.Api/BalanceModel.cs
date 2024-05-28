namespace Balance.Api;

public class BalanceModel
{
    public string FullName { get; set; }
    public string Employer { get; set; }
    public decimal Balance { get; set; }
}

public class ChargeModel
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
}
