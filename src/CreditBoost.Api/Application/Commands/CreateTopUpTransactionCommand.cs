using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Api.Application.Commands;

public sealed class CreateTopUpTransactionCommand : Command
{
    [Required]
    public Guid BeneficiaryId { get; set; }

    [Required]
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    public decimal Amount { get; set; }
}
