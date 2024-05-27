using CreditBoost.Api.Application.Responses;
using CreditBoost.Domain.Interfaces;

namespace CreditBoost.Api.Application.Queries;

public interface ITopUpOptionQuery
{
    Task<IEnumerable<TopUpOptionResponse>> GetAvailable();
}

public class TopUpOptionQuery(ITopUpOptionRepository topUpOptionRepository) : ITopUpOptionQuery
{
    public async Task<IEnumerable<TopUpOptionResponse>> GetAvailable()
    {
        var topUpOptions = await topUpOptionRepository.GetAllAsync();

        return topUpOptions
            .OrderBy(t => t.Amount)
            .Select(t => new TopUpOptionResponse { Amount = t.Amount });
    }
}
