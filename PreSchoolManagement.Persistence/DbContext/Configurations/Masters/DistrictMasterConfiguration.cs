using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class DistrictMasterConfiguration : IEntityTypeConfiguration<DistrictMaster>
{
    public void Configure(EntityTypeBuilder<DistrictMaster> entity)
    {
        entity.ToTable("DistrictMaster");

        entity.HasKey(x => x.DistrictId);
    }
}

public class DistrictTranslationConfiguration : IEntityTypeConfiguration<DistrictTranslation>
{
    public void Configure(EntityTypeBuilder<DistrictTranslation> entity)
    {
        entity.ToTable("DistrictTranslation");

        entity.HasKey(x => x.DistrictTranslationId);

        entity.Property(x => x.LanguageCode)
            .HasMaxLength(5)
            .IsRequired();

        entity.Property(x => x.DistrictName)
            .HasMaxLength(100)
            .IsRequired();

        entity.HasIndex(x => new
        {
            x.DistrictId,
            x.LanguageCode
        }).IsUnique();

        entity.HasOne(x => x.District)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.DistrictId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}