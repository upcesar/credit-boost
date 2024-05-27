using CreditBoost.Domain.Entities;
using CreditBoost.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CreditBoost.Infra.Data.Repositories;

public sealed class UserRepository(CreditBoostDbContext context) : IUserRepository
{
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await context.AppUsers.ToListAsync();
    }

    public async Task<User> GetByAsync(Expression<Func<User, bool>> predicate)
    {
        return await context.AppUsers.SingleOrDefaultAsync(predicate);
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await context.AppUsers.SingleOrDefaultAsync(a => a.Id.Equals(id));
    }

    public async Task<User> GetByIdentityUser(string identityUserId)
    {
        return await context.AppUsers.FirstOrDefaultAsync(a => a.IdentityUserId.Equals(identityUserId));
    }

    public async Task<User> GetByUsername(string userName)
    {
        return await context.AppUsers.FirstOrDefaultAsync(a => a.UserName.Equals(userName));
    }

    public async Task<IEnumerable<User>> GetManyByAsync(Expression<Func<User, bool>> predicate)
    {
        return await context.AppUsers.Where(predicate).ToListAsync();
    }
}
