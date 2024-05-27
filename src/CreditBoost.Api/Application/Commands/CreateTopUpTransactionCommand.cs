namespace CreditBoost.Api.Application.Commands;

public sealed class CreateTopUpTransactionCommand : Command
{
    public Guid BeneficiaryId { get; private set; }
    public decimal Amount { get; private set; }
}
