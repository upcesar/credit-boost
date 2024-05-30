using CreditTopUp.Domain.Entities;
using CreditTopUp.Domain.Interfaces;
using CreditTopUp.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CreditTopUp.Infra.Data.Repositories;

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

    public async Task<User> GetByUsername(string userName)
    {
        return await context.AppUsers.FirstOrDefaultAsync(a => a.UserName.Equals(userName));
    }

    public async Task<IEnumerable<User>> GetManyByAsync(Expression<Func<User, bool>> predicate)
    {
        return await context.AppUsers.Where(predicate).ToListAsync();
    }

    public void Add(User entity)
    {
        context.AppUsers.Add(entity);
    }

    public void Add(IEnumerable<User> entities)
    {
        context.AppUsers.AddRange(entities);
    }

    public void Update(User entity)
    {
        context.AppUsers.Update(entity);
    }

    public void Delete(User entity)
    {
        context.AppUsers.Remove(entity);
    }
}
