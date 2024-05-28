using Polly;
using Polly.Extensions.Http;
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

