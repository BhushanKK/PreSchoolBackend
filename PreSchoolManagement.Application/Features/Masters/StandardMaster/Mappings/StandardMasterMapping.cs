using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class StandardMasterProfile : Profile
{
    public StandardMasterProfile()
    {
        CreateMap<StandardMasterDto, StandardMaster>()
            .ForMember(d => d.StandardId, opt => opt.MapFrom(s => s.StandardId))
            .ForMember(d => d.StandardName, opt => opt.MapFrom(s => s.StandardName ?? string.Empty));
    }
}