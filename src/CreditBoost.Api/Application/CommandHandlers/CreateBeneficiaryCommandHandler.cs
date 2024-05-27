using CreditBoost.Api.Application.Commands;
using CreditBoost.Domain.Entities;
using CreditBoost.Infra.Data.UoW;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Api.Application.CommandHandlers;

public sealed class CreateBeneficiaryCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    : CommandHandler(unitOfWork, httpContextAccessor)
    , IRequestHandler<CreateBeneficiaryCommand, ValidationResult>
{
    public async Task<ValidationResult> Handle(CreateBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        Beneficiary beneficiary = new(
            id: request.Id,
            userId: Guid.Empty,
            nickname: request.Nickname,
            balance: 0m);

        UnitOfWork.Beneficiaries.Add(beneficiary);

        return await SaveChangesAsync();
    }
}
