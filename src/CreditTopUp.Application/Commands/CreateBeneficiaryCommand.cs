using System.ComponentModel.DataAnnotations;

namespace CreditTopUp.Application.Commands;

public sealed class CreateBeneficiaryCommand : Command
{
    [Required]
    [MaxLength(20)]
    public string Nickname { get; set; }
}
