using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class EmployeeTypeMasterProfile :Profile
{
    public EmployeeTypeMasterProfile()
    {
        CreateMap<EmployeeTypeMasterDto ,EmployeeTypeMaster>()
            .ForMember(d => d.EmployeeTypeId, opt => opt.MapFrom(s => s.EmployeeTypeId))
            .ForMember(d => d.EmployeeTypeName, opt => opt.MapFrom(s => s.EmployeeTypeName ?? string.Empty));
    }
}