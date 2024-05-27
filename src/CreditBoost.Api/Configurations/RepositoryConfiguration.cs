using CreditBoost.Domain.Interfaces;
using CreditBoost.Infra.Data.Repositories;
using CreditBoost.Infra.Data.UoW;

namespace CreditBoost.Api.Configurations;

public static class RepositoryConfiguration
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBeneficiaryRepository, BeneficiaryRepository>();
    }
}
