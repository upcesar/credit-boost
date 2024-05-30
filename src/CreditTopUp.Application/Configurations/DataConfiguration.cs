using CreditTopUp.Application.Queries;
using CreditTopUp.Domain.Interfaces;
using CreditTopUp.Infra.Data.Repositories;
using CreditTopUp.Infra.Data.Seeding;
using CreditTopUp.Infra.Data.UoW;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CreditTopUp.Application.Configurations;

public static class DataConfiguration
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBeneficiaryRepository, BeneficiaryRepository>();
        services.AddScoped<ITopUpTransactionRepository, TopUpTransactionRepository>();
        services.AddScoped<ITopUpOptionRepository, TopUpOptionRepository>();
    }

    public static void RegisterQueries(this IServiceCollection services)
    {
        services.AddScoped<IBeneficiaryQuery, BeneficiaryQuery>();
        services.AddScoped<ITopUpOptionQuery, TopUpOptionQuery>();
        services.AddScoped<ITopUpTransactionQuery, TopUpTransactionQuery>();
    }

    public static void RegisterDataSeeding(this IServiceCollection services)
    {
        services.AddScoped<TopUpOptionsSeeding>();
    }

    public static async Task SeedData(this WebApplication app)
    {
        var serviceProvider = app.Services.CreateScope().ServiceProvider;
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var topUpOptionSeeding = scopedProvider.GetService<TopUpOptionsSeeding>();
        await topUpOptionSeeding.Seed();
    }
}
