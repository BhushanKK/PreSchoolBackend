using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class DesignationMasterProfile : Profile
{
    public DesignationMasterProfile()
    {
        CreateMap<DesignationMasterDto,DesignationMaster>();
        CreateMap<DesignationTranslationDto,DesignationTranslation>();
    }
}