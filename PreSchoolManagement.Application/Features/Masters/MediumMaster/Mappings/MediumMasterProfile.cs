using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings ;

public class MediumMasterProfile : Profile
{
    public MediumMasterProfile()
    {
        CreateMap<MediumMasterDto, MediumMaster>()
            .ForMember(d => d.MediumId,opt => opt.MapFrom(s => s.MediumId))
            .ForMember(d => d.Medium,opt => opt.MapFrom(s =>s.Medium));
    }
}