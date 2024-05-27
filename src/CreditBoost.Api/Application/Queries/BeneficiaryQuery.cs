using CreditBoost.Domain.Interfaces;

namespace CreditBoost.Api.Application.Queries;

public interface IBeneficiaryQuery
{
    Task<IEnumerable<string>> GetAll();
}

public class BeneficiaryQuery(IBeneficiaryRepository beneficiaryRepository)
{
    public async Task<IEnumerable<string>> GetAll()
    {
        var berneficiaries = await beneficiaryRepository.GetByUserId(Guid.Empty);
        return null;
    }
}
