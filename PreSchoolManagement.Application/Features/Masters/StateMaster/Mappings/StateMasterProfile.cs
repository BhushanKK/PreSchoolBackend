using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class StateMasterProfile : Profile
{
    public StateMasterProfile()
    {
        CreateMap<StateMasterDto ,StateMaster>()
            .ForMember(d => d.StateId, opt => opt.MapFrom(s => s.StateId))
            .ForMember(d => d.StateName, opt => opt.MapFrom(s => s.StateName ?? string.Empty));
    }
}