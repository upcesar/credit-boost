using CreditBoost.Domain.Entities;
using CreditBoost.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CreditBoost.Infra.Data.Repositories;

public sealed class BeneficiaryRepository(CreditBoostDbContext context) : IBeneficiaryRepository
{
    public async Task<IEnumerable<Beneficiary>> GetAllAsync()
    {
        return await context.Beneficiaries.ToListAsync();
    }

    public async Task<Beneficiary> GetByIdAsync(Guid id)
    {
        return await context.Beneficiaries.SingleOrDefaultAsync(a => a.Id.Equals(id));
    }

    public async Task<IEnumerable<Beneficiary>> GetByUser(Guid userId)
    {
        return await context.Beneficiaries
            .Where(a => a.UserId.Equals(userId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Beneficiary>> GetByUserId(Guid userId)
    {
        return await context.Beneficiaries
            .Where(a => a.UserId.Equals(userId))
            .ToListAsync();
    }

    public async Task<Beneficiary> GetByAsync(Expression<Func<Beneficiary, bool>> predicate)
    {
        return await context.Beneficiaries.SingleOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Beneficiary>> GetManyByAsync(Expression<Func<Beneficiary, bool>> predicate)
    {
        return await context.Beneficiaries
            .Where(predicate)
            .ToListAsync();
    }

    public void Add(Beneficiary entity)
    {
        context.Beneficiaries.Add(entity);
    }

    public void Update(Beneficiary entity)
    {
        context.Beneficiaries.Update(entity);
    }

    public void Delete(Beneficiary entity)
    {
        context.Beneficiaries.Remove(entity);
    }
}
