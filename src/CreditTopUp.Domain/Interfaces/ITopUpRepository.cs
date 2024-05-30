using CreditTopUp.Domain.Entities;

namespace CreditTopUp.Domain.Interfaces;

public interface ITopUpTransactionRepository : IRepository<TopUpTransaction>
{
    Task<IEnumerable<TopUpTransaction>> GetMonthlyByUser(Guid userId);
}
public interface ITopUpOptionRepository : IRepository<TopUpOption> { }
