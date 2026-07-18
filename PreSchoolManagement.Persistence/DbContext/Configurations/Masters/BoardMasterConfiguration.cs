using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class BoardMasterConfiguration : IEntityTypeConfiguration<BoardMaster>
{
    public void Configure(EntityTypeBuilder<BoardMaster> entity)
    {
        entity.ToTable("BoardMaster");

        entity.HasKey(x => x.BoardId);
    }
}

public class BoardTranslationConfiguration : IEntityTypeConfiguration<BoardTranslation>
{
    public void Configure(EntityTypeBuilder<BoardTranslation> entity)
    {
        entity.ToTable("BoardTranslation");

        entity.HasKey(x => x.BoardTranslationId);

        entity.Property(x => x.LanguageCode)
            .HasMaxLength(5)
            .IsRequired();

        entity.Property(x => x.BoardName)
            .HasMaxLength(100)
            .IsRequired();

        entity.HasIndex(x => new
        {
            x.BoardId,
            x.LanguageCode
        }).IsUnique();

        entity.HasOne(x => x.Board)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.BoardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}