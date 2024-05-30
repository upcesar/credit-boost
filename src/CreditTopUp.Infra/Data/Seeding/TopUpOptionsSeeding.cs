using CreditTopUp.Domain.Entities;
using CreditTopUp.Infra.Data.UoW;

namespace CreditTopUp.Infra.Data.Seeding;
public class TopUpOptionsSeeding(IUnitOfWork unitOfWork)
{
    public async Task Seed()
    {
        var topUpOptions = await unitOfWork.TopUpOptionRepository.GetAllAsync() ?? Enumerable.Empty<TopUpOption>();

        if (topUpOptions.Any()) return;

        IEnumerable<decimal> topUpAmmounts = [5, 10, 20, 30, 50, 75, 100];

        topUpOptions = topUpAmmounts.Select(amount => new TopUpOption(Guid.NewGuid(), amount));

        unitOfWork.TopUpOptionRepository.Add(topUpOptions);
        _ = await unitOfWork.CommitAsync();
    }
}
