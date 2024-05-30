namespace CreditTopUp.Domain.Views;
public class MonthlyTopUpTransactions
{
    public Guid BeneficiaryId { get; set; }
    public string Nickname { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal Amount { get; set; }
}
