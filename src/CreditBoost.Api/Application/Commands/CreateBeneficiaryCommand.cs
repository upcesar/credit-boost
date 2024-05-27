namespace CreditBoost.Api.Application.Commands;

public sealed class CreateBeneficiaryCommand : Command
{
    public string Nickname { get; private set; }
    public decimal Balance { get; private set; }
}
