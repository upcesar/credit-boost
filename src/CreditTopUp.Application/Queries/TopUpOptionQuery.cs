using CreditTopUp.Application.Responses;
using CreditTopUp.Domain.Interfaces;

namespace CreditTopUp.Application.Queries;

public interface ITopUpOptionQuery
{
    Task<IEnumerable<TopUpOptionResponse>> GetAvailables();
}

public sealed class TopUpOptionQuery(ITopUpOptionRepository topUpOptionRepository) : ITopUpOptionQuery
{
    public async Task<IEnumerable<TopUpOptionResponse>> GetAvailables()
    {
        var topUpOptions = await topUpOptionRepository.GetAllAsync();

        return topUpOptions
            .OrderBy(t => t.Amount)
            .Select(t => new TopUpOptionResponse { Amount = t.Amount });
    }
}
