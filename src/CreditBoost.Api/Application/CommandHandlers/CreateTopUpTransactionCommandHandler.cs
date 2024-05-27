using CreditBoost.Api.Application.Commands;
using CreditBoost.Domain.Entities;
using CreditBoost.Infra.Auth.Models;
using CreditBoost.Infra.Data.UoW;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Api.Application.CommandHandlers;

public sealed class CreateTopUpTransactionCommandHandler(IUnitOfWork unitOfWork, IAuthenticatedUser authenticatedUser)
    : CommandHandler(unitOfWork, authenticatedUser)
    , IRequestHandler<CreateTopUpTransactionCommand, ValidationResult>
{
    private const int maxBeneficiary = 5;

    public async Task<ValidationResult> Handle(CreateTopUpTransactionCommand request, CancellationToken cancellationToken)
    {


        TopUpTransaction transaction = new(
            id: request.Id,
            beneficiaryId: request.BeneficiaryId,
            amount: request.Amount,
            charge: 1);

        UnitOfWork.TopUpTransactionRepository.Add(transaction);
        return await SaveChangesAsync();
    }
}
