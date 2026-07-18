using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class DivisionMasterProfile : Profile
{
    public DivisionMasterProfile()
    {
        CreateMap<DivisionMasterDto, DivisionMaster>();
        CreateMap<DivisionTranslationDto, DivisionTranslation>();
    }
}