using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class SectionMasterConfiguration : IEntityTypeConfiguration<SectionMaster>
{
    public void Configure(EntityTypeBuilder<SectionMaster> entity)
    {
        entity.ToTable("SectionMaster");
        entity.HasKey(e => e.SectionId);
    }
}
public class SectionTranslationConfiguration : IEntityTypeConfiguration<SectionTranslation>
{
    public void Configure(EntityTypeBuilder<SectionTranslation> entity)
    {
        entity.ToTable("SectionTranslation");

        entity.HasKey(x => x.SectionTranslationId);

        entity.Property(x => x.LanguageCode)
            .HasMaxLength(5)
            .IsRequired();

        entity.Property(x => x.SectionName)
            .HasMaxLength(100)
            .IsRequired();

        entity.HasIndex(x => new
        {
            x.SectionId,
            x.LanguageCode
        }).IsUnique();

        entity.HasOne(x => x.Section)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.SectionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
