using CreditTopUp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditTopUp.Infra.Data.Configurations;

public sealed class TopUpTransactionEntityConfiguration : IEntityTypeConfiguration<TopUpTransaction>
{
    public void Configure(EntityTypeBuilder<TopUpTransaction> builder)
    {
        builder.Property(f => f.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(f => f.Charge)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(f => f.TotalAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.ToTable("TopUpTransactions");
    }
}
