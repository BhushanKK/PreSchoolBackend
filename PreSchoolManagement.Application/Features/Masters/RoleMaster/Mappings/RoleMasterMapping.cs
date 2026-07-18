using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class RoleMasterProfile : Profile
{
    public RoleMasterProfile()
    {
        CreateMap<RoleTranslationDto, RoleTranslation>();
        CreateMap<RoleMasterDto, RoleMaster>();
    }
}