using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings ;

public class MediumMasterProfile : Profile
{
    public MediumMasterProfile()
    {
        CreateMap<MediumMasterDto, MediumMaster>();
        CreateMap<MediumTranslationDto,MediumTranslation>();
        
    }
}