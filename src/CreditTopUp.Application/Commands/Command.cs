using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CreditTopUp.Application.Commands;

public abstract class Command : IRequest<ValidationResult>
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
}
