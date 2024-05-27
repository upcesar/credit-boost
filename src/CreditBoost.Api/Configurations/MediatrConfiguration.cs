using CreditBoost.Api.Application.CommandHandlers;
using CreditBoost.Api.Application.Commands;
using CreditBoost.Api.Application.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CreditBoost.Api.Configurations;

public static class MediatrConfiguration
{
    public static void RegisterCommandHandlers(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddScoped<IRequestHandler<RegisterUserCommand, ValidationResult>, RegisterUserCommandHandler>();
        services.AddScoped<IRequestHandler<LoginCommand, AuthenticationResponse>, LoginCommandHandler>();

        services.AddScoped<IRequestHandler<CreateBeneficiaryCommand, ValidationResult>, CreateBeneficiaryCommandHandler>();
    }
}
