using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class EmployeeTypeMasterConfiguration : IEntityTypeConfiguration<EmployeeTypeMaster>
{
    public void Configure(EntityTypeBuilder<EmployeeTypeMaster> entity)
    {
        entity.ToTable("EmployeeTypeMaster");
        entity.HasKey(e => e.EmployeeTypeId);
    }
}
