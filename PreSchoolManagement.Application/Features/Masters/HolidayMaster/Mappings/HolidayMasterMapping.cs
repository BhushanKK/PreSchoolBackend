using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class HolidayMasterProfile : Profile
{
    public HolidayMasterProfile()
    {
        CreateMap<HolidayMasterDto, HolidayMaster>();
        CreateMap<HolidayTranslationDto, HolidayTranslation>();
        
    }
}