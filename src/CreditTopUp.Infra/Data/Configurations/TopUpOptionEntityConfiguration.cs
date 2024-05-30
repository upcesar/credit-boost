using CreditTopUp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditTopUp.Infra.Data.Configurations;

public sealed class TopUpOptionEntityConfiguration : IEntityTypeConfiguration<TopUpOption>
{
    public void Configure(EntityTypeBuilder<TopUpOption> builder)
    {
        builder.Property(f => f.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.ToTable("TopUpOptions");
    }
}
