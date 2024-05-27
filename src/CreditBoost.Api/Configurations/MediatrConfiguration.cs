using CreditBoost.Api.Application.CommandHandlers;
using CreditBoost.Api.Application.Commands;
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
    }
}
