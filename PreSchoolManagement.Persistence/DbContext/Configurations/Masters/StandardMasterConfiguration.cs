using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class StandardMasterConfiguration : IEntityTypeConfiguration<StandardMaster>
{
    public void Configure(EntityTypeBuilder<StandardMaster> entity)
    {
        entity.ToTable("StandardMaster");
        entity.HasKey(e => e.StandardId);
    }
}

public class StandardTranslationConfiguration : IEntityTypeConfiguration<StandardTranslation>
{
    public void Configure(EntityTypeBuilder<StandardTranslation>entity)
    {
        entity.ToTable("StandardTranslation");

        entity.HasKey(x => x.StandardTranslationId);

        entity.Property(x => x.LanguageCode)
            .HasMaxLength(5)
            .IsRequired();
        
        entity.Property(x => x.StandardName)
            .HasMaxLength(100)
            .IsRequired();

        entity.HasIndex(x => new
        {
            x.StandardId,
            x.LanguageCode
        }).IsUnique();

        entity.HasOne(x => x.Standard)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.StandardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
