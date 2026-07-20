using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class MediumMasterConfiguration : IEntityTypeConfiguration<MediumMaster>
{
    public void Configure(EntityTypeBuilder<MediumMaster> entity)
    {
        entity.ToTable("MediumMaster");
        entity.HasKey(e => e.MediumId);
    }
}

public class MediumTranslationConfiguration : IEntityTypeConfiguration<MediumTranslation>
{
    public void Configure(EntityTypeBuilder<MediumTranslation>entity)
    {   
        entity.ToTable("MediumTranslation");
        entity.HasKey(x => x.MediumTranslationId);
        entity.Property(x => x.LanguageCode)
            .HasMaxLength(5)
            .IsRequired();
        entity.Property(x => x.MediumName)
            .HasMaxLength(100)
            .IsRequired();
        
        entity.HasIndex(x => new
        {
            x.MediumId,
            x.LanguageCode
        }).IsUnique();

        entity.HasOne(x => x.Medium)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.MediumId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
