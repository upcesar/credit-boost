using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Application.Commands;

public abstract class Command : IRequest<ValidationResult>
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
}
