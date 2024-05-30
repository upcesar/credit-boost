using CreditTopUp.Domain.Entities;

namespace CreditTopUp.Domain.Interfaces;

public interface IBeneficiaryRepository : IRepository<Beneficiary>
{
    Task<IEnumerable<Beneficiary>> GetByUserId(Guid userId);
}
