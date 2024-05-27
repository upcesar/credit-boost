using CreditBoost.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditBoost.Infra.Data.Configurations;

public sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u => u.Beneficiaries)
               .WithOne(b => b.User)
               .HasForeignKey(b => b.UserId);

        builder.ToTable("Users");
    }
}
