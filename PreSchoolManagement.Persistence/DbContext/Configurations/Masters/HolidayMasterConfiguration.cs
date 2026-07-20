using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class HolidayMasterConfiguration : IEntityTypeConfiguration<HolidayMaster>
{
    public void Configure(EntityTypeBuilder<HolidayMaster> entity)
    {
        entity.ToTable("HolidayMaster");
        entity.HasKey(e => e.HolidayId);
    }
}

public class HolidayTranslationConfiguration : IEntityTypeConfiguration<HolidayTranslation>
{
    public void Configure(EntityTypeBuilder<HolidayTranslation> entity)
    {
        entity.ToTable("HolidayTranslation");
        entity.HasKey(x => x.HolidayTranslationId);
        entity.Property(x => x.LanguageCode)
            .HasMaxLength(5)
            .IsRequired();
        entity.Property(x => x.HolidayName)
            .HasMaxLength(100)
            .IsRequired();
        entity.HasIndex(x => new
        {
            x.HolidayId,
            x.HolidayName
        }).IsUnique();
        entity.HasOne(x => x.Holiday)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.HolidayId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}