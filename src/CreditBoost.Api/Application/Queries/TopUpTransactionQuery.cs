using CreditBoost.Api.Application.Responses;
using CreditBoost.Domain.Interfaces;
using CreditBoost.Domain.Views;

namespace CreditBoost.Api.Application.Queries;

public interface ITopUpTransactionQuery
{
    Task<TopUpTransactionResponse> GetById(Guid id);
    Task<IEnumerable<MonthlyTopUpTransactions>> GetMonthlyTopUpTransactions(Guid userId);
    Task<IEnumerable<MonthlyTopUpTransactions>> GetMonthlyTopUpTransactionsByBeneficiary(Guid userId, Guid beneficiaryId);
}

public sealed class TopUpTransactionQuery(ITopUpTransactionRepository topUpTransactionRepository) : ITopUpTransactionQuery
{
    public async Task<TopUpTransactionResponse> GetById(Guid id)
    {
        var transaction = await topUpTransactionRepository.GetByIdAsync(id);
        return transaction is null ? null : new TopUpTransactionResponse
        {
            BeneficiaryId = transaction.BeneficiaryId,
            Amount = transaction.Amount,
            Charge = transaction.Charge,
            TotalAmount = transaction.TotalAmount,
            Timestamp = transaction.Timestamp,
        };
    }

    public async Task<IEnumerable<MonthlyTopUpTransactions>> GetMonthlyTopUpTransactions(Guid userId)
    {
        var transactions = await topUpTransactionRepository.GetMonthlyByUser(userId);

        return transactions
            .GroupBy(t => new
            {
                t.Timestamp.Year,
                t.Timestamp.Month,
                t.BeneficiaryId,
                t.Beneficiary.Nickname
            })
            .Select(group => new MonthlyTopUpTransactions
            {
                Year = group.Key.Year,
                Month = group.Key.Month,
                BeneficiaryId = group.Key.BeneficiaryId,
                Nickname = group.Key.Nickname,
                Amount = group.Sum(t => t.TotalAmount)
            });
    }

    public async Task<IEnumerable<MonthlyTopUpTransactions>> GetMonthlyTopUpTransactionsByBeneficiary(Guid userId, Guid beneficiaryId)
    {
        var transactions = await GetMonthlyTopUpTransactions(userId);
        return transactions.Where(t => t.BeneficiaryId.Equals(beneficiaryId));
    }
}
