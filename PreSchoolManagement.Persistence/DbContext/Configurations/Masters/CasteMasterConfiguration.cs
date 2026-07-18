using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class CasteMasterConfiguration : IEntityTypeConfiguration<CasteMaster>
{
    public void Configure(EntityTypeBuilder<CasteMaster> entity)
    {
        entity.ToTable("CasteMaster");

        entity.HasKey(x => x.CasteID);
    }
}

public class CasteTranslationConfiguration : IEntityTypeConfiguration<CasteTranslation>
{
    public void Configure(EntityTypeBuilder<CasteTranslation> entity)
    {
        entity.ToTable("CasteTranslation");

        entity.HasKey(x => x.CasteTranslationId);

        entity.Property(x => x.LanguageCode)
            .HasMaxLength(5)
            .IsRequired();

        entity.Property(x => x.CasteName)
            .HasMaxLength(100)
            .IsRequired();

        entity.HasIndex(x => new
        {
            x.CasteID,
            x.LanguageCode
        }).IsUnique();

        entity.HasOne(x => x.Caste)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.CasteID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}