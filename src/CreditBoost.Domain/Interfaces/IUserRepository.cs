using CreditBoost.Domain.Entities;

namespace CreditBoost.Domain.Interfaces;

public interface IUserRepository : IReadonlyRepository<User>
{
    Task<User> GetByIdentityUser(string identityUserId);
    Task<User> GetByUsername(string userName);
}
