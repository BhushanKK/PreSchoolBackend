using AutoMapper;
using SchoolAdmission.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Application.Features.Mappings;

public class CasteMasterProfile : Profile
{
    public CasteMasterProfile()
    {
        CreateMap<CasteMasterDto, CasteMaster>()
            .ForMember(d => d.CasteID, opt => opt.MapFrom(s => s.CasteId))
            .ForMember(d => d.CategoryID, opt => opt.MapFrom(s => s.CategoryId ?? 0))
            .ForMember(d => d.CasteName, opt => opt.MapFrom(s => s.Caste ?? string.Empty));
    }
}
