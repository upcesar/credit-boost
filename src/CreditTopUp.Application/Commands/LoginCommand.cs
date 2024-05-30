using CreditTopUp.Application.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CreditTopUp.Application.Commands;

public class LoginCommand : IRequest<AuthenticationResponse>
{
    [Required(ErrorMessage = "User Name is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
