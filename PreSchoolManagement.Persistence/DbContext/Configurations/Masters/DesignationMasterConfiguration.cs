using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class DesignationMasterConfiguration : IEntityTypeConfiguration<DesignationMaster>
{
    public void Configure(EntityTypeBuilder<DesignationMaster> entity)
    {
        entity.ToTable("DesignationMaster");

        entity.HasKey(x => x.DesignationId);
    }
}

public class DesignationTranslationConfiguration : IEntityTypeConfiguration<DesignationTranslation>
{
    public void Configure(EntityTypeBuilder<DesignationTranslation> entity)
    {
        entity.ToTable("DesignationTranslation");

        entity.HasKey(x => x.DesignationTranslationId);

        entity.Property(x => x.LanguageCode)
            .HasMaxLength(5)
            .IsRequired();

        entity.Property(x => x.Designation)
            .HasMaxLength(100)
            .IsRequired();

        entity.HasIndex(x => new
        {
            x.DesignationId,
            x.LanguageCode
        }).IsUnique();

        entity.HasOne(x => x.DesignationMaster)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.DesignationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}