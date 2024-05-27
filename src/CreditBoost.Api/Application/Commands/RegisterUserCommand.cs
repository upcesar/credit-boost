using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Api.Application.Commands;

public sealed class RegisterUserCommand : Command
{
    [Required(ErrorMessage = "User Name is required")]
    public string UserName { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
