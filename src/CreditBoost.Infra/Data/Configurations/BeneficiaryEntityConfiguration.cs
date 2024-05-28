using CreditBoost.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditBoost.Infra.Data.Configurations;

public sealed class BeneficiaryEntityConfiguration : IEntityTypeConfiguration<Beneficiary>
{
    public void Configure(EntityTypeBuilder<Beneficiary> builder)
    {
        builder.HasMany(u => u.TopUpTransactions)
               .WithOne(b => b.Beneficiary)
               .HasForeignKey(b => b.BeneficiaryId);

        builder.Property(f => f.Nickname)
            .HasColumnType("varchar(20)")
            .HasMaxLength(20)
            .IsRequired();

        builder.ToTable("Beneficiaries");
    }
}
