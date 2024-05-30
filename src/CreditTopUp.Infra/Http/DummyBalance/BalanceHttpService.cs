using CreditTopUp.Infra.Http;
using CreditTopUp.Infra.Http.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace CreditTopUp.Infra.Http.DummyBalance;

public interface IBalanceHttpService
{
    Task<bool> ChargeAsync(Guid userId, decimal amount);
    Task<BalanceModel> GetBalanceAsync(Guid userId);
}

public class BalanceHttpService(ResilientHttpClient resilientHttpClient, IOptions<HttpServiceSettings> options) : IBalanceHttpService
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

