using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class FinancialYearMasterConfiguration : IEntityTypeConfiguration<FinancialYearMaster>
{
    public void Configure(EntityTypeBuilder<FinancialYearMaster> entity)
    {
        entity.ToTable("FinancialYearMaster");
        entity.HasKey(e => e.FinancialYearId);
    }
}

public class FinancialYearTranslationConfigure : IEntityTypeConfiguration<FinancialYearTranslation>
{
    public void Configure(EntityTypeBuilder<FinancialYearTranslation> entity)
    {
        entity.ToTable("FinancialYearTranslation");
        entity.HasKey(x => x.FinancialYearTranslationId);
        entity.Property(x => x.LanguageCode)
            .HasMaxLength(5)
            .IsRequired();
        entity.Property(x => x.FinancialYearName)
            .HasMaxLength(100)
            .IsRequired();
        entity.HasIndex(x => new
        {
            x.FinancialYearId,
            x.LanguageCode
        }).IsUnique();

        entity.HasOne(x => x.FinancialYear)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.FinancialYearId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}