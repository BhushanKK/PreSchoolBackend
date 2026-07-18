using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class SectionMasterProfile : Profile
{
    public SectionMasterProfile()
    {
        CreateMap<SectionTranslationDto, SectionTranslation>();
        CreateMap<SectionMasterDto, SectionMaster>();
    }
}