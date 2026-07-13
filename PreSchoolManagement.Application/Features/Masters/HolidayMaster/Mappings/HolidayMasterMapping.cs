using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class HolidayMasterProfile : Profile
{
    public HolidayMasterProfile()
    {
        CreateMap<HolidayMasterDto, HolidayMaster>()
            .ForMember(d => d.HolidayId, opt => opt.MapFrom(s => s.HolidayId))
            .ForMember(d => d.HolidayName, opt => opt.MapFrom(s => s.HolidayName ?? string.Empty));
    }
}