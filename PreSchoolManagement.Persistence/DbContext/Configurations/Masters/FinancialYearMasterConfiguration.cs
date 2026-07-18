using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class FinancialYearMasterConfiguration : IEntityTypeConfiguration<FinancialYearMaster>
{
    public void Configure(EntityTypeBuilder<FinancialYearMaster> entity)
    {
        entity.ToTable("FinancialYearMaster");
        entity.HasKey(e => e.FinancialYearId);
    }
}
