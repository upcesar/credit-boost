using CreditBoost.Api.Application.Commands;
using CreditBoost.Api.Application.Queries;
using CreditBoost.Domain.Constants;
using CreditBoost.Domain.Entities;
using CreditBoost.Infra.Auth.Models;
using CreditBoost.Infra.Data.UoW;
using CreditBoost.Infra.Http.DummyBalance;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Api.Application.CommandHandlers;

public sealed class CreateTopUpTransactionCommandHandler(
    IUnitOfWork unitOfWork,
    ITopUpOptionQuery topUpOptionQuery,
    ITopUpTransactionQuery topUpTransactionQuery,
    IAuthenticatedUser authenticatedUser,
    IBalanceHttpService balanceHttpService)
    : CommandHandler(unitOfWork, authenticatedUser)
    , IRequestHandler<CreateTopUpTransactionCommand, ValidationResult>
{
    public async Task<ValidationResult> Handle(CreateTopUpTransactionCommand request, CancellationToken cancellationToken)
    {
        TopUpTransaction transaction = new(
            id: request.Id,
            beneficiaryId: request.BeneficiaryId,
            amount: request.Amount,
            charge: 1);

        if (await ValidateInsufficientBalance(transaction.TotalAmount))
            return new ValidationResult("Insufficient balance");

        if (await ValidateUnavailablesTopUpOptions(transaction.Amount))
            return new ValidationResult("The amount is not available in top up options");

        if (await ChargeAmount(transaction.TotalAmount))
            return new ValidationResult("Error on charging transaction amount.");

        if (await CheckBeneficiaryMonthlyLimit(transaction))
            return new ValidationResult("The user has reached the monthly limit for top ups");

        UnitOfWork.TopUpTransactionRepository.Add(transaction);
        return await SaveChangesAsync();
    }

    private async Task<bool> ValidateInsufficientBalance(decimal amount)
    {
        var externalServiceResult = await balanceHttpService.GetBalanceAsync(CurrentUserId);
        return externalServiceResult.Balance < amount;
    }

    private async Task<bool> ValidateUnavailablesTopUpOptions(decimal amount)
    {
        var topUpsAvailables = await topUpOptionQuery.GetAvailables();
        var availableAmounts = topUpsAvailables.Select(options => options.Amount);

        return !availableAmounts.Contains(amount);
    }

    private async Task<bool> ChargeAmount(decimal amount)
    {
        return await balanceHttpService.ChargeAsync(CurrentUserId, amount);
    }

    private async Task<bool> CheckBeneficiaryMonthlyLimit(TopUpTransaction transaction)
    {
        var transactions = await topUpTransactionQuery.GetMonthlyTopUpTransactionsByBeneficiary(CurrentUserId, transaction.BeneficiaryId);

        var maxAmountForTopUp = await PickMonthlyMaxAmount();
        return transactions.Any(t => t.Amount > maxAmountForTopUp);
    }

    private async Task<decimal> PickMonthlyMaxAmount()
    {
        var user = await UnitOfWork.Users.GetByIdAsync(CurrentUserId);

        if (user is User { IsVerified: true })
        {
            return MaximumTopUpAmounts.VerifiedUsers;
        }

        return MaximumTopUpAmounts.UnverifiedUsers;
    }
}
