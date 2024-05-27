using CreditBoost.Infra.Data.UoW;
using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Api.Application.CommandHandlers;

public abstract class CommandHandler(IUnitOfWork unitOfWork)
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    protected IUnitOfWork UnitOfWork => unitOfWork;

    protected CommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : this(unitOfWork)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected async Task<ValidationResult> SaveChangesAsync()
    {
        if (!await unitOfWork.CommitAsync())
        {
            return new ValidationResult("Error on saving data");
        }

        return ValidationResult.Success;
    }

    private async Task<string> GetUserName()
    {
        var user = _httpContextAccessor.HttpContext.User;
        if (user is not null && user.Identity.IsAuthenticated)
        {
            return user.Identity.Name;
        }

        return string.Empty;
    }
}
