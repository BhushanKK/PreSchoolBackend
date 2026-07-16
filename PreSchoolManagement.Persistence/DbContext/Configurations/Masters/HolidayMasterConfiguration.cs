using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class HolidayMasterConfiguration : IEntityTypeConfiguration<HolidayMaster>
{
    public void Configure(EntityTypeBuilder<HolidayMaster> entity)
    {
        entity.ToTable("HolidayMaster");
        entity.HasKey(e => e.HolidayId);
    }
}
