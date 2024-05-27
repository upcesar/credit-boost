using CreditBoost.Domain.Entities;

namespace CreditBoost.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByUsername(string userName);
}
