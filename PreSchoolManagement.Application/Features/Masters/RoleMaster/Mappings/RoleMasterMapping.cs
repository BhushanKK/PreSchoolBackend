using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class RoleMasterProfile : Profile
{
    public RoleMasterProfile()
    {
        CreateMap<RoleMasterDto, RoleMaster>()
            .ForMember(d => d.RoleId, opt => opt.MapFrom(s => s.RoleId))
            .ForMember(d => d.RoleName, opt => opt.MapFrom(s => s.RoleName ?? string.Empty));
    }
}