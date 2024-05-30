using CreditBoost.Application.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Application.Commands;

public class LoginCommand : IRequest<AuthenticationResponse>
{
    [Required(ErrorMessage = "User Name is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
