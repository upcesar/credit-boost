using CreditBoost.Api.Application.Commands;
using CreditBoost.Api.Application.Queries;
using CreditBoost.Domain.Entities;
using CreditBoost.Infra.Auth.Models;
using CreditBoost.Infra.Data.UoW;
using CreditBoost.Infra.Http;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Api.Application.CommandHandlers;

public sealed class CreateTopUpTransactionCommandHandler(IUnitOfWork unitOfWork, ITopUpOptionQuery topUpOptionQuery, IAuthenticatedUser authenticatedUser, BalanceHttpService balanceHttpService)
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
}
