using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class DivisionMasterProfile : Profile
{
    public DivisionMasterProfile()
    {
        CreateMap<DivisionMasterDto, DivisionMaster>()
            .ForMember(d => d.DivisionId, opt => opt.MapFrom(s => s.DivisionId))
            .ForMember(d => d.DivisionName, opt => opt.MapFrom(s => s.DivisionName ?? string.Empty));
    }
}