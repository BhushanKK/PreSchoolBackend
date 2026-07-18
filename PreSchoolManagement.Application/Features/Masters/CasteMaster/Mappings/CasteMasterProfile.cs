using AutoMapper;
using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Application.Features.Mappings;

public class CasteMasterProfile : Profile
{
    public CasteMasterProfile()
    {
        CreateMap<CasteTranslationDto, CasteTranslation>()
            .ForMember(
                d => d.CasteName,
                opt => opt.MapFrom(s => s.Caste));

        CreateMap<CasteMasterDto, CasteMaster>()
            .ForMember(
                d => d.CasteName,
                opt => opt.MapFrom(s => s.Caste))
            .ForMember(
                d => d.Translations,
                opt => opt.Ignore());
    }
}
