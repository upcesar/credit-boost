using CreditBoost.Domain.Interfaces;

namespace CreditBoost.Infra.Data.UoW;

public interface IUnitOfWork
{
    IBeneficiaryRepository Beneficiaries { get; }
    ITopUpOptionRepository TopUpOptionRepository { get; }
    IUserRepository Users { get; }
    Task<bool> CommitAsync();
}

public class UnitOfWork(
    CreditBoostDbContext context,
    IBeneficiaryRepository beneficiaryRepository,
    ITopUpOptionRepository topUpOptionRepository,
    IUserRepository userRepository
    ) : IUnitOfWork
{
    public IBeneficiaryRepository Beneficiaries => beneficiaryRepository;
    public ITopUpOptionRepository TopUpOptionRepository => topUpOptionRepository;
    public IUserRepository Users => userRepository;

    public async Task<bool> CommitAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
