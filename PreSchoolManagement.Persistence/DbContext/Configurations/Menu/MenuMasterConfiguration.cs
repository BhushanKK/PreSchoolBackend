using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class MenuMasterConfiguration : IEntityTypeConfiguration<MenuMaster>
{
    public void Configure(EntityTypeBuilder<MenuMaster> entity)
    {
        entity.ToTable("MenuMaster");
        entity.HasKey(e => e.MenuId);
    }
}

public class MenuTranslationConfiguration : IEntityTypeConfiguration<MenuTranslation>
{
    public void Configure(EntityTypeBuilder<MenuTranslation> entity)
    {
        entity.ToTable("MenuTranslation");

        entity.HasKey(x => x.MenuTranslationId);

        entity.Property(x => x.LanguageCode)
            .HasMaxLength(5)
            .IsRequired();

        entity.Property(x => x.MenuName)
            .HasMaxLength(100)
            .IsRequired();

        entity.HasIndex(x => new
        {
            x.MenuId,
            x.LanguageCode
        }).IsUnique();

        entity.HasOne(x => x.MenuMaster)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.MenuId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}