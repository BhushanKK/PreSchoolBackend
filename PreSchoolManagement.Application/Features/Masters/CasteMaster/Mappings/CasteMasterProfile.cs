using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class CasteMasterProfile : Profile
{
    public CasteMasterProfile()
    {
        CreateMap<CasteMasterDto, CasteMaster>();
        CreateMap<CasteTranslationDto, CasteTranslation>();
    }
}
