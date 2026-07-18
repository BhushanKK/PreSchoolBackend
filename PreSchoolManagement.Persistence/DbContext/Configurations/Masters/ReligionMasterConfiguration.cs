using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class ReligionMasterConfiguration : IEntityTypeConfiguration<ReligionMaster>
{
    public void Configure(EntityTypeBuilder<ReligionMaster> entity)
    {
        entity.ToTable("ReligionMaster");
        entity.HasKey(e => e.ReligionId);
    }
}

public class ReligionTranslationConfiguration : IEntityTypeConfiguration<ReligionTranslation>
{
    public void Configure(EntityTypeBuilder<ReligionTranslation>entity)
    {
        entity.ToTable("ReligionTranslation");
        entity.HasKey(x => x.ReligionTranslationId);
        entity.Property(x =>x.LanguageCode)
            .HasMaxLength(5)
            .IsRequired();
        entity.Property(x => x.ReligionName)
            .HasMaxLength(100)
            .IsRequired();
        entity.HasIndex(x => new
        {
            x.ReligionId,
            x.ReligionName
        }).IsUnique();
        entity.HasOne(x => x.Religion)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.ReligionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}