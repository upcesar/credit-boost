using CreditTopUp.Domain.Interfaces;
using CreditTopUp.Infra.Data;

namespace CreditTopUp.Infra.Data.UoW;

public interface IUnitOfWork
{
    IBeneficiaryRepository Beneficiaries { get; }
    ITopUpOptionRepository TopUpOptionRepository { get; }
    ITopUpTransactionRepository TopUpTransactionRepository { get; }
    IUserRepository Users { get; }
    Task<bool> CommitAsync();
}

public class UnitOfWork(
    CreditBoostDbContext context,
    IBeneficiaryRepository beneficiaryRepository,
    ITopUpTransactionRepository topUpTransactionRepository,
    ITopUpOptionRepository topUpOptionRepository,
    IUserRepository userRepository
    ) : IUnitOfWork
{
    public IBeneficiaryRepository Beneficiaries => beneficiaryRepository;
    public ITopUpTransactionRepository TopUpTransactionRepository => topUpTransactionRepository;
    public ITopUpOptionRepository TopUpOptionRepository => topUpOptionRepository;
    public IUserRepository Users => userRepository;

    public async Task<bool> CommitAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
