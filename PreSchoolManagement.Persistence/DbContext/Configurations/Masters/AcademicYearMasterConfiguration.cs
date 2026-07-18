using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class AcademicYearMasterConfiguration : IEntityTypeConfiguration<AcademicYearMaster>
{
    public void Configure(EntityTypeBuilder<AcademicYearMaster> entity)
    {
        entity.ToTable("AcademicYearMaster");
        entity.HasKey(x => x.AcademicYearId);
    }
}

public class AcademicYearTranslationConfiguration : IEntityTypeConfiguration<AcademicYearTranslation>
{
    public void Configure(EntityTypeBuilder<AcademicYearTranslation> entity)
    {
        entity.ToTable("AcademicYearTranslation");
        entity.HasKey(x => x.AcademicYearTranslationId);
        entity.Property(x => x.LanguageCode)
            .HasMaxLength(5)
            .IsRequired();

        entity.Property(x => x.AcademicYearName)
            .HasMaxLength(100)
            .IsRequired();

        entity.HasIndex(x => new
        {
            x.AcademicYearId,
            x.LanguageCode
        }).IsUnique();

        entity.HasOne(x => x.AcademicYear)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.AcademicYearId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}