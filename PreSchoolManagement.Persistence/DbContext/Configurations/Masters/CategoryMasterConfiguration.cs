using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class CategoryMasterConfiguration : IEntityTypeConfiguration<CategoryMaster>
{
    public void Configure(EntityTypeBuilder<CategoryMaster> entity)
    {
        entity.ToTable("CategoryMaster");
        entity.HasKey(x => x.CategoryId);
    }
}

public class CategoryTranslationConfiguration : IEntityTypeConfiguration<CategoryTranslation>
{
    public void Configure(EntityTypeBuilder<CategoryTranslation> entity)
    {
        entity.ToTable("CategoryTranslation");
        entity.HasKey(x => x.CategoryTranslationId);
        entity.Property(x => x.LanguageCode)
            .HasMaxLength(5)
            .IsRequired();
        entity.Property(x => x.CategoryName)
            .HasMaxLength(100)
            .IsRequired();
        entity.HasIndex(x => new
        {
            x.CategoryId,
            x.LanguageCode
        }).IsUnique();
        entity.HasOne(x => x.Category)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}