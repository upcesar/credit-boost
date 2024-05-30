using CreditTopUp.Infra.Http.DummyBalance;
using CreditTopUp.Infra.Http.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CreditTopUp.Infra.Http;

public static class ExternalServiceConfiguration
{
    public static void RegisterHttpServices(this IServiceCollection services, IConfiguration configuration)
    {
        var httpSection = configuration.GetSection("HttpServices");
        services.Configure<HttpServiceSettings>(httpSection);

        services.AddHttpClient<ResilientHttpClient>();
        services.AddScoped<IBalanceHttpService, BalanceHttpService>();
    }
}

