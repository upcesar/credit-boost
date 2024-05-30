using CreditTopUp.Domain.Entities;
using CreditTopUp.Domain.Interfaces;
using CreditTopUp.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CreditTopUp.Infra.Data.Repositories;

public sealed class TopUpTransactionRepository(CreditBoostDbContext context) : ITopUpTransactionRepository
{
    public async Task<IEnumerable<TopUpTransaction>> GetAllAsync()
    {
        return await context.TopUpTransactions.ToListAsync();
    }

    public async Task<TopUpTransaction> GetByIdAsync(Guid id)
    {
        var t = await context.TopUpTransactions.ToListAsync();
        return await context.TopUpTransactions.SingleOrDefaultAsync(a => a.Id.Equals(id));
    }

    public async Task<TopUpTransaction> GetByAsync(Expression<Func<TopUpTransaction, bool>> predicate)
    {
        return await context.TopUpTransactions.SingleOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<TopUpTransaction>> GetManyByAsync(Expression<Func<TopUpTransaction, bool>> predicate)
    {
        return await context.TopUpTransactions
            .Where(predicate)
            .ToListAsync();
    }
    public async Task<IEnumerable<TopUpTransaction>> GetMonthlyByUser(Guid userId)
    {
        return await context.TopUpTransactions
            .AsNoTracking()
            .Include(t => t.Beneficiary)
            .ThenInclude(b => b.User)
            .Where(t => t.Beneficiary.User.Id.Equals(userId))
            .ToListAsync();
    }

    public void Add(TopUpTransaction entity)
    {
        context.TopUpTransactions.Add(entity);
    }
    public void Add(IEnumerable<TopUpTransaction> entities)
    {
        context.TopUpTransactions.AddRange(entities);
    }

    public void Update(TopUpTransaction entity)
    {
        context.TopUpTransactions.Update(entity);
    }

    public void Delete(TopUpTransaction entity)
    {
        context.TopUpTransactions.Remove(entity);
    }

}
