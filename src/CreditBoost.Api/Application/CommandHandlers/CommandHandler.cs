using CreditBoost.Infra.Auth.Models;
using CreditBoost.Infra.Data.UoW;
using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Api.Application.CommandHandlers;

public abstract class CommandHandler(IUnitOfWork unitOfWork)
{
    private readonly IAuthenticatedUser _authenticatedUser;

    protected IUnitOfWork UnitOfWork => unitOfWork;

    protected Guid CurrentUserId => _authenticatedUser.UserId;

    protected CommandHandler(IUnitOfWork unitOfWork, IAuthenticatedUser authenticatedUser) : this(unitOfWork)
    {
        _authenticatedUser = authenticatedUser;
    }

    protected async Task<ValidationResult> SaveChangesAsync()
    {
        if (!await unitOfWork.CommitAsync())
        {
            return new ValidationResult("Error on saving data");
        }

        return ValidationResult.Success;
    }


}
