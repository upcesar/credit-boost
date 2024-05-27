using CreditBoost.Domain.Entities;
using CreditBoost.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CreditBoost.Infra.Data.Repositories;

public sealed class TopUpOptionRepository(CreditBoostDbContext context) : ITopUpOptionRepository
{
    public async Task<IEnumerable<TopUpOption>> GetAllAsync()
    {
        return await context.TopUpOptions.ToListAsync();
    }

    public async Task<TopUpOption> GetByIdAsync(Guid id)
    {
        return await context.TopUpOptions.SingleOrDefaultAsync(a => a.Id.Equals(id));
    }

    public async Task<TopUpOption> GetByAsync(Expression<Func<TopUpOption, bool>> predicate)
    {
        return await context.TopUpOptions.SingleOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<TopUpOption>> GetManyByAsync(Expression<Func<TopUpOption, bool>> predicate)
    {
        return await context.TopUpOptions
            .Where(predicate)
            .ToListAsync();
    }

    public void Add(TopUpOption entity)
    {
        context.TopUpOptions.Add(entity);
    }
    public void Add(IEnumerable<TopUpOption> entities)
    {
        context.TopUpOptions.AddRange(entities);
    }

    public void Update(TopUpOption entity)
    {
        context.TopUpOptions.Update(entity);
    }

    public void Delete(TopUpOption entity)
    {
        context.TopUpOptions.Remove(entity);
    }
}
