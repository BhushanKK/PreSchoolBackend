using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class ChairmanMasterProfile : Profile
{
    public ChairmanMasterProfile()
    {
        CreateMap<ChairmanMasterDto, ChairmanMaster>();
        CreateMap<ChairmanTranslationDto, ChairmanTranslation>();
    }
}
