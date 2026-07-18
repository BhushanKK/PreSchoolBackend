using AutoMapper;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class DistrictMasterProfile : Profile
{
    public DistrictMasterProfile()
    {
        CreateMap<DistrictMasterDto, DistrictMaster>();
        CreateMap<DistrictTranslationDto, DistrictTranslation>();
    }
}