using System.Reflection;

namespace CreditBoost.Api.Configurations;

public static class MediatrConfiguration
{
    public static void RegisterCommandHandlers(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    }
}
