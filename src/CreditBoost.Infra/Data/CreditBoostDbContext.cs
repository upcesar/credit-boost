using CreditBoost.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CreditBoost.Infra.Data;

public class CreditBoostDbContext(DbContextOptions<CreditBoostDbContext> options) : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<User> AppUsers { get; set; }
    public DbSet<Beneficiary> Beneficiaries { get; set; }
    public DbSet<TopUpTransaction> TopUpTransactions { get; set; }
    public DbSet<TopUpOption> TopUpOptions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(CreditBoostDbContext).Assembly);
    }
}
