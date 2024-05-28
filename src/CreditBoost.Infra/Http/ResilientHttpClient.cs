using CreditBoost.Infra.Http.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

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

    public async Task<HttpResponseMessage> PostAsync<T>(string uri, T data = default)
    {
        var content = SerializeContent(data);
        return await RetryPolicy.ExecuteAsync(() => httpClient.PostAsync(uri, content));
    }

    private StringContent SerializeContent<T>(T data)
    {
        var json = JsonSerializer.Serialize(data);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}

public class BalanceHttpService(ResilientHttpClient resilientHttpClient, IOptions<HttpServiceSettings> options)
{
    public async Task<BalanceModel> GetBalanceAsync(Guid userId)
    {
        var response = await resilientHttpClient.GetAsync($"{options.Value.Balance}/balance/{userId}");
        return await response.Content.ReadFromJsonAsync<BalanceModel>();
    }

    public async Task<bool> ChargeAsync(Guid userId, decimal amount)
    {
        var response = await resilientHttpClient.PostAsync($"{options.Value.Balance}/charge/{userId}", new { userId, amount });
        return response.IsSuccessStatusCode && await response.Content.ReadFromJsonAsync<bool>();
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
    public decimal Balance { get; set; }
}

