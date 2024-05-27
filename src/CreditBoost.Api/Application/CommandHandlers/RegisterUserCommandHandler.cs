using CreditBoost.Api.Application.Commands;
using CreditBoost.Domain.Entities;
using CreditBoost.Infra.Data.UoW;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Api.Application.CommandHandlers;

public sealed class RegisterUserCommandHandler(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
    : CommandHandler(unitOfWork), IRequestHandler<RegisterUserCommand, ValidationResult>
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
            Id = request.Id.ToString(),
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

        User appUser = new(request.Id, request.UserName);
        UnitOfWork.Users.Add(appUser);

        return await SaveChangesAsync();
    }
}
