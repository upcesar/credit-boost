using CreditTopUp.Domain.Entities;

namespace CreditTopUp.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByUsername(string userName);
}
