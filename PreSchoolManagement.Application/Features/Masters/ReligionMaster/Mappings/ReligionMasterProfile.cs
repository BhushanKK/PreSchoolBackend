using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class ReligionMasterProfile : Profile
{
    public ReligionMasterProfile()
    {
        CreateMap<ReligionMasterDto, ReligionMaster>()
            .ForMember(d => d.ReligionId, opt => opt.MapFrom(s => s.ReligionId))            
            .ForMember(d => d.Religion, opt => opt.MapFrom(s => s.Religion ?? string.Empty));
    }
}