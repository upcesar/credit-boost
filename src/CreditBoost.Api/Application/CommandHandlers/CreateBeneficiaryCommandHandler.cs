using CreditBoost.Api.Application.Commands;
using CreditBoost.Domain.Entities;
using CreditBoost.Infra.Auth.Models;
using CreditBoost.Infra.Data.UoW;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Api.Application.CommandHandlers;

public sealed class CreateBeneficiaryCommandHandler(IUnitOfWork unitOfWork, IAuthenticatedUser authenticatedUser)
    : CommandHandler(unitOfWork, authenticatedUser)
    , IRequestHandler<CreateBeneficiaryCommand, ValidationResult>
{
    public async Task<ValidationResult> Handle(CreateBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        Beneficiary beneficiary = new(
            id: request.Id,
            userId: CurrentUserId,
            nickname: request.Nickname,
            balance: decimal.Zero);

        UnitOfWork.Beneficiaries.Add(beneficiary);

        return await SaveChangesAsync();
    }
}
