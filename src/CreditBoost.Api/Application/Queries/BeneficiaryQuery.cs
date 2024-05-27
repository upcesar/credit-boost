using CreditBoost.Api.Application.Responses;
using CreditBoost.Domain.Interfaces;
using CreditBoost.Infra.Auth.Models;

namespace CreditBoost.Api.Application.Queries;

public interface IBeneficiaryQuery
{
    Task<IEnumerable<BeneficiaryResponse>> GetAvailables();
}

public class BeneficiaryQuery(IBeneficiaryRepository beneficiaryRepository, IAuthenticatedUser authenticatedUser)
    : IBeneficiaryQuery
{

    public async Task<IEnumerable<BeneficiaryResponse>> GetAvailables()
    {
        var beneficiaries = await beneficiaryRepository.GetByUserId(authenticatedUser.UserId);

        return beneficiaries?.Select(b => new BeneficiaryResponse
        {
            Nickname = b.Nickname,
            Balance = b.Balance,
        });
    }
}
