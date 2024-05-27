using CreditBoost.Api.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Api.Application.CommandHandlers;

public sealed class RegisterUserCommandHandler(UserManager<IdentityUser> userManager)
    : IRequestHandler<RegisterUserCommand, ValidationResult>
{
    public async Task<ValidationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await userManager.FindByNameAsync(request.UserName);

        if (userExists is not null)
        {
            return new ValidationResult("User already exists!");
        }

        IdentityUser user = new()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.UserName
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return new ValidationResult("User creation went wrong", errors);
        }

        return ValidationResult.Success;
    }
}
