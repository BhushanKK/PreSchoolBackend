using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class DivisionMasterConfiguration : IEntityTypeConfiguration<DivisionMaster>
{
    public void Configure(EntityTypeBuilder<DivisionMaster> entity)
    {
        entity.ToTable("DivisionMaster");
        entity.HasKey(x => x.DivisionId);
    }
}

public class DivisionTranslationConfiguration : IEntityTypeConfiguration<DivisionTranslation>
{
    public void Configure(EntityTypeBuilder<DivisionTranslation> entity)
    {
        entity.ToTable("DivisionTranslation");
        entity.HasKey(x => x.DivisionTranslationId);
        entity.Property(x => x.LanguageCode)
            .HasMaxLength(5)
            .IsRequired();

        entity.Property(x => x.DivisionName)
            .HasMaxLength(100)
            .IsRequired();

        entity.HasIndex(x => new
        {
            x.DivisionId,
            x.LanguageCode
        }).IsUnique();

        entity.HasOne(x => x.Division)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.DivisionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}