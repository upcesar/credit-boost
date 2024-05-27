using CreditBoost.Api.Application.Queries;
using CreditBoost.Domain.Interfaces;
using CreditBoost.Infra.Data.Repositories;
using CreditBoost.Infra.Data.UoW;

namespace CreditBoost.Api.Configurations;

public static class DataConfiguration
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBeneficiaryRepository, BeneficiaryRepository>();
    }

    public static void RegisterQueries(this IServiceCollection services)
    {
        services.AddScoped<IBeneficiaryQuery, BeneficiaryQuery>();
    }
}
