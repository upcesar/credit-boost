using CreditBoost.Domain.Entities;

namespace CreditBoost.Domain.Interfaces;

public interface ITopUpTransactionRepository : IRepository<TopUpTransaction>
{
    Task<IEnumerable<TopUpTransaction>> GetMonthlyByUser(Guid userId);
}
public interface ITopUpOptionRepository : IRepository<TopUpOption> { }
