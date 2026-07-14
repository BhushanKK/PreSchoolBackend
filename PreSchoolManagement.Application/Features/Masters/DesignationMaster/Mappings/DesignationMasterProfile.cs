using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class DesignationMasterProfile : Profile
{
    public DesignationMasterProfile()
    {
        CreateMap<DesignationMasterDto,DesignationMaster>()
            .ForMember(d => d.DesignationId,opt => opt.MapFrom(s => s.DesignationId))
            .ForMember(d => d.Designation,opt => opt.MapFrom(s => s.Designation ?? string.Empty));
    }
}