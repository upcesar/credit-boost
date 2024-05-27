using CreditBoost.Domain.Entities;

namespace CreditBoost.Domain.Interfaces;

public interface IBeneficiaryRepository : IRepository<Beneficiary>
{
    Task<IEnumerable<Beneficiary>> GetByUserId(Guid userId);
}
