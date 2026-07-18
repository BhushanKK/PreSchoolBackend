using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class ReligionMasterProfile : Profile
{
    public ReligionMasterProfile()
    {
        CreateMap<ReligionMasterDto,ReligionMaster>();
        CreateMap<ReligionTranslationDto,ReligionTranslation>();
    }
}