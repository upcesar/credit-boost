using CreditTopUp.Application.Responses;
using CreditTopUp.Domain.Interfaces;
using CreditTopUp.Infra.Auth.Models;

namespace CreditTopUp.Application.Queries;

public interface IBeneficiaryQuery
{
    Task<IEnumerable<BeneficiaryResponse>> GetAvailables();
    Task<BeneficiaryResponse> GetById(Guid id);
}

public class BeneficiaryQuery(IBeneficiaryRepository beneficiaryRepository, IAuthenticatedUser authenticatedUser)
    : IBeneficiaryQuery
{

    public async Task<IEnumerable<BeneficiaryResponse>> GetAvailables()
    {
        var beneficiaries = await beneficiaryRepository.GetByUserId(authenticatedUser.UserId);

        return beneficiaries?.Select(b => new BeneficiaryResponse
        {
            Nickname = b.Nickname
        });
    }

    public async Task<BeneficiaryResponse> GetById(Guid id)
    {
        var benerficiary = await beneficiaryRepository.GetByIdAsync(id);
        return benerficiary is null ? null : new BeneficiaryResponse
        {
            Nickname = benerficiary.Nickname
        };
    }
}
