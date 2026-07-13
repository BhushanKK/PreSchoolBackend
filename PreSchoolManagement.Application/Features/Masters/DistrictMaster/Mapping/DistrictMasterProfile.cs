using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class DistrictMasterProfile : Profile
{
    public DistrictMasterProfile()
    {
        CreateMap<DistrictMasterDto, DistrictMaster>()
            .ForMember(d => d.DistrictId, opt => opt.MapFrom(s => s.DistrictId))
            .ForMember(d => d.StateId, opt => opt.MapFrom(s =>s.StateId?? 0))
            .ForMember(d => d.DistrictName, opt => opt.MapFrom(s => s.DistrictName ?? string.Empty));
    }
}