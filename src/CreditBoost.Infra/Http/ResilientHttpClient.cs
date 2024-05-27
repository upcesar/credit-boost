using CreditBoost.Infra.Http.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using System.Net.Http.Json;

namespace CreditBoost.Infra.Http;

public class ResilientHttpClient(HttpClient httpClient)
{
    // Define the retry policy
    private static IAsyncPolicy<HttpResponseMessage> RetryPolicy => HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => (int)msg.StatusCode == 429)
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    public async Task<HttpResponseMessage> GetAsync(string uri)
    {
        return await RetryPolicy.ExecuteAsync(() => httpClient.GetAsync(uri));
    }
}

public class BalanceHttpService(ResilientHttpClient resilientHttpClient, IOptions<HttpServiceSettings> options)
{
    public async Task<BalanceModel> GetAsync(Guid id)
    {
        var response = await resilientHttpClient.GetAsync($"{options.Value.Balance}/balance/{id}");
        return await response.Content.ReadFromJsonAsync<BalanceModel>();
    }
}

public static class ExternalServiceConfiguration
{
    public static void RegisterHttpServices(this IServiceCollection services, IConfiguration configuration)
    {
        var httpSection = configuration.GetSection("HttpServices");
        services.Configure<HttpServiceSettings>(httpSection);

        services.AddHttpClient<ResilientHttpClient>();
        services.AddScoped<BalanceHttpService>();
    }
}


public class BalanceModel
{
    public string FullName { get; set; }
    public string Employer { get; set; }
    public decimal Amount { get; set; }
}
