using CreditBoost.Domain.Interfaces;
using CreditBoost.Infra.Auth.Models;

namespace CreditBoost.Api.Application.Queries;

public interface IBeneficiaryQuery
{
    Task<IEnumerable<BeneficiaryDto>> GetAvailables();
}

public sealed class BeneficiaryDto
{
    public string Nickname { get; set; }
    public decimal Balance { get; set; }
}

public class BeneficiaryQuery(IBeneficiaryRepository beneficiaryRepository, IAuthenticatedUser authenticatedUser)
    : IBeneficiaryQuery
{

    public async Task<IEnumerable<BeneficiaryDto>> GetAvailables()
    {
        var beneficiaries = await beneficiaryRepository.GetByUserId(authenticatedUser.UserId);

        return beneficiaries?.Select(b => new BeneficiaryDto
        {
            Nickname = b.Nickname,
            Balance = b.Balance,
        });
    }
}
