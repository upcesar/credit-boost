using CreditTopUp.Application.Commands;
using CreditTopUp.Domain.Entities;
using CreditTopUp.Infra.Auth.Models;
using CreditTopUp.Infra.Data.UoW;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CreditTopUp.Application.CommandHandlers;

public sealed class CreateBeneficiaryCommandHandler(IUnitOfWork unitOfWork, IAuthenticatedUser authenticatedUser)
    : CommandHandler(unitOfWork, authenticatedUser)
    , IRequestHandler<CreateBeneficiaryCommand, ValidationResult>
{
    private const int maxBeneficiary = 5;

    public async Task<ValidationResult> Handle(CreateBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var beneficiaries = await UnitOfWork.Beneficiaries.GetByUserId(CurrentUserId);

        if (beneficiaries.Count() >= maxBeneficiary)
            return new ValidationResult($"Use can only add up to {maxBeneficiary} beneficiaries");

        if (beneficiaries.Any(b => b.Nickname == request.Nickname))
            return new ValidationResult("The beneficiary already exists in the database");

        Beneficiary beneficiary = new(
            id: request.Id,
            userId: CurrentUserId,
            nickname: request.Nickname);

        UnitOfWork.Beneficiaries.Add(beneficiary);
        return await SaveChangesAsync();
    }
}
