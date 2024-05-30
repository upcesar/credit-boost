using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CreditTopUp.Application.Configurations;

public static class MediatrConfiguration
{
    public static void RegisterCommandHandlers(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    }
}
