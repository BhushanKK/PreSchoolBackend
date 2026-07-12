using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class SectionMasterProfile : Profile
{
    public SectionMasterProfile()
    {
        CreateMap<SectionMasterDto, SectionMaster>()
            .ForMember(d => d.SectionId, opt => opt.MapFrom(s => s.SectionId))
            .ForMember(d => d.SectionName, opt => opt.MapFrom(s => s.SectionName ?? string.Empty));
    }
}