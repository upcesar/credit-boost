using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Api.Application.Commands;

public abstract class Command : IRequest<ValidationResult>
{
    public Guid Id { get; set; } = Guid.NewGuid();
}
